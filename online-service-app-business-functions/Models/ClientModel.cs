namespace online_service_app_business_functions.Models
{
    public class ClientModel
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Patronymic { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;
    }
}
