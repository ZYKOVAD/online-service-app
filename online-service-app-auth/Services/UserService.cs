using online_service_app_auth.db_layer;
using online_service_app_auth.Interfaces;
using online_service_app_auth.models;
using System.Net;
using System.Security.Cryptography;

namespace online_service_app_auth.Services
{
    public class UserService
    {
        private readonly PasswordHasher _hasher;
        private readonly JwtProvider _jwt;
        
        public UserService(PasswordHasher hasher, JwtProvider jwt)
        {
            _hasher = hasher;
            _jwt = jwt;
        }

        public string Login(string password, IUser user)
        {
            var result = _hasher.Verify(password, user.Password);
            if (result == false)
            {
                throw new Exception("Ошибка входа. Проверьте пароль.");
            }

            var token = _jwt.GenerateToken(user);

            return token;
        }
    }
}
