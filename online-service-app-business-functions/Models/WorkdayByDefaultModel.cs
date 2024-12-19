namespace online_service_app_business_functions.Models
{
    public class WorkdayByDefaultModel
    { 
        public TimeOnly TimeStart { get; set; }

        public TimeOnly TimeEnd { get; set; }

        public TimeOnly? BreakStart { get; set; }

        public TimeOnly? BreakEnd { get; set; }
    }
}
