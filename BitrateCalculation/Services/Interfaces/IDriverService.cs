using BitrateCalculation.Models;

namespace BitrateCalculation.Services.Interfaces
{
    public interface IDriverService
    {
        Driver<NetworkInterfaceCardWithBitrates> CalculateBitrate(Driver<NetworkInterfaceCard> driver);
    }
}
