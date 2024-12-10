namespace online_service_app_auth.models
{
    public class ClientRequestModel
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Patronymic { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
