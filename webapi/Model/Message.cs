namespace webapi.Model
{
    public class Message
    {
        public string Topic { get; set; }

        public string DeviceId { get; set; }

        public string TimeStamp { get; set; }

        public string Payload { get; set; }
    }

}
