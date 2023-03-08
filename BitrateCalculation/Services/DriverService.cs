using BitrateCalculation.Configuration;
using BitrateCalculation.Helpers;
using BitrateCalculation.Models;
using BitrateCalculation.Services.Interfaces;

namespace BitrateCalculation.Services
{
    public class DriverService : IDriverService
    {

        private readonly IConfiguration _configuration;
        public DriverService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Driver<NetworkInterfaceCardWithBitrates> CalculateBitrate(Driver<NetworkInterfaceCard> driver)
        {
            var pollingConfig = _configuration.GetSection("PollingRates").Get<PollingRateConfiguration>();
            
            var newNic = new List<NetworkInterfaceCardWithBitrates>();

            foreach (var networkInterfaceCard in driver.NIC)
            {
                newNic.Add(NetworkInterfaceCardMapper(networkInterfaceCard, pollingConfig.PollingRate));
            }

            return new Driver<NetworkInterfaceCardWithBitrates>
            {
                Device = driver.Device,
                Model = driver.Model,
                NIC = newNic
            };
        }

        private NetworkInterfaceCardWithBitrates NetworkInterfaceCardMapper(NetworkInterfaceCard nic, string pollingRate)
        {
            
            return new NetworkInterfaceCardWithBitrates
            {
                Description = nic.Description,
                MAC = nic.MAC,
                Timestamp = nic.Timestamp,
                Rx = nic.Rx,
                Tx = nic.Tx,
                RxBitrate = BitrateCalculator.CalculateBitrate(nic.Rx, pollingRate),
                TxBitrate = BitrateCalculator.CalculateBitrate(nic.Tx, pollingRate)
            };
        }
    }
}
