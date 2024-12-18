namespace online_service_app_business_functions.Models
{
    public class RabbitMqModelMessage
    {
        public RabbitMqModelMessage(string nameFunc, long milliseconds)
        {
            Id = Guid.NewGuid();
            NameFunction = nameFunc;
            Milliseconds = milliseconds;
        }
        public Guid Id { get; set; }
        public string NameFunction { get; set; }
        public long Milliseconds { get; set; }
    }
}
