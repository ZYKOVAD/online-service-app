using System.Security.Cryptography;

namespace online_service_app_business_functions.Services
{
    public class RsaService
    {
        private readonly IConfiguration _configuration;

        public RsaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RSA GetRsa()
        {
            var publicKey = _configuration["PublicKey"];
            using var rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);
            return rsa;
        }
    }
}
