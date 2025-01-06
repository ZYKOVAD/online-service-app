using Microsoft.AspNetCore.Identity;

namespace online_service_app_auth.Services
{
    public class PasswordHasher
    {
        public string Generate(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hashedPassword;
        }

        public bool Verify(string password, string hashedPassword)
        {
            bool result = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
            return result;
        }
    }
}
