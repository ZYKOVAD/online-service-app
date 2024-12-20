namespace online_service_app_business_functions.Models
{
    public class WorkdayModel
    {
        public DateOnly Date { get; set; }

        public TimeOnly TimeStart { get; set; }

        public TimeOnly TimeEnd { get; set; }

        public TimeOnly? BreakStart { get; set; }

        public TimeOnly? BreakEnd { get; set; }
    }
}
