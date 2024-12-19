using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.DbLayer;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public int? SphereId { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? WebAddress { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual SphereOfOrganization? Sphere { get; set; }

    public virtual TypeOfOrganization Type { get; set; } = null!;
}
