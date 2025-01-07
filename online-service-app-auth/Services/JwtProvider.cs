using Microsoft.IdentityModel.Tokens;
using online_service_app_auth.db_layer;
using online_service_app_auth.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace online_service_app_auth.Services
{
    public class JwtProvider
    {
        public string GenerateToken(IUser user)
        {
            string privateKey;
            using (StreamReader reader = new StreamReader("PrivateKey.key"))
            {
                privateKey = reader.ReadToEnd();
            }

            using var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey.ToCharArray());

            Claim[] claims =
                [
                    new Claim("Id", user.Id.ToString()),
                    new Claim("typeUser", user.GetType().ToString())
                ];

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
