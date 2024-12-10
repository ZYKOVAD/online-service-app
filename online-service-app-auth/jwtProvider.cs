using Microsoft.IdentityModel.Tokens;
using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace online_service_app_auth
{
    public class jwtProvider
    {
        public string GenerateToken(IUser user)
        {
            Claim[] claims = 
                [
                    new("Id", user.Id.ToString()),
                    new("typeUser", user.GetType().ToString())
                ];
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("online-service-app-online-service-app-online-service-app")),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(2));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
