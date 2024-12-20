namespace online_service_app_business_functions.Models
{
    public class ServiceModel
    {
        public string Name { get; set; } = null!;
        public short Duration { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

    }
}
