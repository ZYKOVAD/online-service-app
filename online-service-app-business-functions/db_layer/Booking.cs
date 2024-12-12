using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.db_layer;

public partial class Booking
{
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
