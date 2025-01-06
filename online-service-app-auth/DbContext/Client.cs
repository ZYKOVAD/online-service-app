using online_service_app_auth.Interfaces;
using System;
using System.Collections.Generic;

namespace online_service_app_auth.db_layer;

public partial class Client : IUser
{ 
    public Client(int id, string name, string surname, string? patronymic, string? phone, string email, string password)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Phone = phone;
        Email = email;
        Password = password;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }
    
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
