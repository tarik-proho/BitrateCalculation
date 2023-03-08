namespace BitrateCalculation.Models
{
    public class Driver<T> where T : class
    {
        public string Device { get; set; }
        public string Model { get; set; }
        public List<T> NIC { get; set; }
    }
}
