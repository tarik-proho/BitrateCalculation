using BitrateCalculation.Models;

namespace BitrateCalculation.Helpers
{
    public static class BitrateCalculator
    {
        public static decimal CalculateBitrate(string octet, string pollingRate)
        {
            if (Decimal.TryParse(octet, out decimal decimalOctet) && Decimal.TryParse(pollingRate, out decimal decimalPollingRate))
            {
                return decimalOctet / decimalPollingRate * 8;
            }

            throw new InvalidDataException($"One or more invalid values! Octet value: {octet}, Polling rate value: {pollingRate}");
        }
    }
}
