namespace audit_service
{
    public class Db
    {
        public static List<RabbitMqModelMesssage> Data = new List<RabbitMqModelMesssage>();
    }

    public class RabbitMqModelMesssage
    {
        public Guid Id { get; set; }
        public string? NameFunction { get; set; }
        public long Milliseconds { get; set; }
    }
}
