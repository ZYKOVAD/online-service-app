namespace online_service_app_auth.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
}
