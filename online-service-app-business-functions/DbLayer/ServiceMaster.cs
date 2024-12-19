namespace online_service_app_business_functions.DbLayer
{
    public class ServiceMaster
    {
        public int ServiceId { get; set; }

        public int MasterId { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual Master Master { get; set; } = null!;
    }
}
