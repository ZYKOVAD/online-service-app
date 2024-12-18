using Microsoft.IdentityModel.Tokens;
using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace online_service_app_auth
{
    public class jwtProvider
    {
        public string GenerateToken(IUser user)
        {
            var privateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAm+6A0cvML3dDEDNvWu6HnmSHu5TVX+mdLhLkL3juu1RLjsFG
efrNvhmVEqwhp2m2oDUYG2y0RoFsMPuEfguUfqPq/Tgiu7cHcRQSXEhBV+WufNgF
MbeUf5K90MHjnGygsbUuZoxLH8dQqOft+ZYmynhk0rKbumRONlC9G9qTRes0bG4T
XRT1H/+QBeYWK71OzSM4pKwr9Z+1FTc7ZIYslExWVy0w5tZdN4v2sUWHoQhK9DgZ
GdHgnxLYsdIfNoXi6TMVqLyGfh0B5hDIfX0hPZPfMesVGuOwYXUDFJakl3sjfH9C
OEUiTALA8YpAeh+HWqkdTCTea4mFnkNaFneYiQIDAQABAoIBAAh9XaMj3ZaZ/4Xo
HwB26zMFqy7chW+G6HQOkEDrbH1jiLJENfeZ3vjM1hp2/RAV3TJvdu61+5djytsH
M/6pgmi7Y6XCGCtbOuWpCkTe+ZEfuWElC8daBOpF4t10/lT7xGtYf3xoLVuUvlYB
O/+HnCNllwEHu3B/BJPIJTuuf1qM7tGtYL7JjUFQe33DZwl0v4gg67OZpcuQ+qN9
X/IW5O7kr3hf1uxtNkIk/Jwji8ZjHIXp3PDtXAupfmST50daRBY8KFLUgcMh+Dt7
oXPBDKWTX3JXFu7YZLxTUpBjaLW15Je9regFo8Ia3PxiOqb5Axa+V5AKo0k+jPZ6
/v1WgXkCgYEA0hEkVdhdE8oTWId+9FaWxoe2p/OrTnY16bfnuuBfTKKZ9vBhyBYX
OnWEWrKGRJhhYJ01FgaU7wgX/ABIYb0je9rpn6JgtE3OgfvEuMUSa0jB6ji5zT1D
fFUs4NmqmK3jGZYldal9m8idPNAelRtQWDDm8I9Qf7adOXNSgY07DwUCgYEAvgcL
b1PvFdnbj9SsKmqWhBhJoBdfA8tiUXJ5elATelsSCwxlmuYLmm2FEvO/NU+wMF+q
TyDlXkheMJhDKi+mk73CTNsSic+1ZHbI4R4zIIZ8b0W03chdnh63WOKX4coN6zHw
aAD+tqkvjpZyLwhnaXAiRJPTF0TCt9TlnqxbMrUCgYEAgWosH3LvTLzGlFjNXsxl
kSOU5P2aQPlUl4tECP4n1/eosNhA9VqertYtVw765xGVlEblqI8fe+9Zj1fBP+2d
CbXBDiakOxBgM/YeqNJIWa32QUz3MrkWdWeoAVI35l3iNiYpJ8bmam36aLeyz5uH
MP3XmMZCCpw/WmBwqP2QL90CgYANf1SZmL2fsLS+t51u+dktEsiP/Xj7PQQDwvzL
6kon47YZYM0KvpJOar8MBJItSDa8iN8A9dY05zEBhJ8orO/JCK53ZQwhC8bXlvRX
wRxikArgoHPjWOsFYyskvhuJpx9r+EUaLg/pi4TcPOVpX9Bwlc1pT5Kr0W9/PTj+
F0H3DQKBgQC8w7ZpUsB6lzvGe61lXz5a6Yyq8NsmE/ulP4dK7o1Q2sUtvc2mxe/B
mS+EuRZRo2Ya/xwJhFlvqZv92SCJJAndcQFhLUTrW5hmmRz9gOiP/tlg/erpkhfC
T3IiTDqO9CLp5G4bDESombUqOSaTpkg+nucty938Yk4Z9YKJ2bpGRg==
-----END RSA PRIVATE KEY-----";

            using var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

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
