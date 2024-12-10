using System;
using System.Collections.Generic;

namespace online_service_app_auth.db_layer;

public partial class BookingStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
