using System;
using System.Collections.Generic;

namespace online_service_app_business_functions.db_layer;

public partial class Master
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int SpecializationId { get; set; }

    public int? OrganizationId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Organization? Organization { get; set; }

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual ICollection<WorkdayByDefault> WorkdayByDefaults { get; set; } = new List<WorkdayByDefault>();

    public virtual ICollection<Workday> Workdays { get; set; } = new List<Workday>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
