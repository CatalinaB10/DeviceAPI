namespace DeviceAPI.Models
{
    public class DeviceDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double MaxEnergyConsumption { get; set; }
        public long UserId { get; set; }
    }
}
