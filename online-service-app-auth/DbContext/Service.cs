using System;
using System.Collections.Generic;

namespace online_service_app_auth.db_layer;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OrganizationId { get; set; }

    public short Duration { get; set; }

    public int Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Organization Organization { get; set; } = null!;
}
