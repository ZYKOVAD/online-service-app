using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.DbLayer;

public partial class Service
{
    public Service() { }
    public Service(int id, string name, int organizationId, short duration, int price, string? description)
    {
        Id = id;
        Name = name;
        OrganizationId = organizationId;
        Duration = duration;
        Price = price;
        Description = description;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OrganizationId { get; set; }

    public short Duration { get; set; }

    public int Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();
}
