namespace webapi.Model
{
    public class Payload
    {
        public string DeviceId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public String Measurement { get; set; }
        public String UnitOfMeasurement { get; set; }

    }
}
