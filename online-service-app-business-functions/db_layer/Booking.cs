namespace online_service_app_business_functions.db_layer;

public partial class Booking
{
    public Booking() { }
    public Booking(int id, int orgId, int clinetId, DateTime dateTime, int masterId, int serviceId)
    {
        Id = id;
        OrganizationId = orgId;
        ClientId = clinetId;
        DateTime = dateTime;
        MasterId = masterId;
        ServiceId = serviceId;
        StatusId = 1;
    }
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int ClientId { get; set; }

    public DateTime DateTime { get; set; }

    public int MasterId { get; set; }

    public int ServiceId { get; set; }

    public int StatusId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Master Master { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual BookingStatus Status { get; set; } = null!;
}
