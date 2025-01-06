using online_service_app_auth.Interfaces;
using System;
using System.Collections.Generic;

namespace online_service_app_auth.db_layer;

public partial class Master : IUser 
{
    private Master() { }
    public Master(int id, string name, string surname, string? patronymic, string? phone, string email, string password, int spId, int orgId)
    {
        Id = id; 
        Name = name; 
        Surname = surname; 
        Patronymic = patronymic; 
        Phone = phone; 
        Email = email;
        Password = password;
        SpecializationId = spId;
        OrganizationId = orgId;
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int SpecializationId { get; set; }

    public int OrganizationId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual ICollection<WorkdayByDefault> WorkdayByDefaults { get; set; } = new List<WorkdayByDefault>();

    public virtual ICollection<Workday> Workdays { get; set; } = new List<Workday>();

}
