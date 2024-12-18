namespace online_service_app_business_functions.Models
{
    public class BookingModel
    {
        public int OrganizationId { get; set; }

        public int ClientId { get; set; }

        public DateTime DateTime { get; set; }

        public int MasterId { get; set; }

        public int ServiceId { get; set; }

        public int StatusId { get; set; }
    }
}
