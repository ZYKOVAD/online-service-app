using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using System.Net;
using System.Security.Cryptography;

namespace online_service_app_auth
{
    public class UserService
    {
        private readonly PasswordHasher hasher;
        private readonly OnlineServiceDbContext db;
        private readonly jwtProvider jwt;
        private static int countClientId;
        private static int countMasterId;
        private static int countOrganizationId;
        public UserService(PasswordHasher _hasher, OnlineServiceDbContext _db, jwtProvider _jwt)
        {
            hasher = _hasher;
            db = _db;
            jwt = _jwt;
            countClientId = db.Clients.Max(c => c.Id);
            countMasterId = db.Masters.Max(c => c.Id);
            countOrganizationId = db.Organizations.Max(c => c.Id);
        }

        // метод регистрации клиента
        public Client RegisterClient(ClientRequestModel cl)
        {
            var hashedPassword = hasher.Generate(cl.Password);
            countClientId += 1;
            Client client = Client.Create(countClientId, cl.Name, cl.Surname, cl.Patronymic, cl.Phone, cl.Email, hashedPassword);
            db.Clients.Add(client);
            db.SaveChanges();
            return client;   
        }

        // метод регистрации мастера
        public Master RegisterMaster(MasterRequestModel m)
        {
            var hashedPassword = hasher.Generate(m.Password);
            countMasterId += 1;
            Master master = Master.Create(countMasterId, m.Name, m.Surname, m.Patronymic, m.Phone, m.Email, hashedPassword, m.SpecializationId, m.OrganizationId);
            db.Masters.Add(master);
            db.SaveChanges();
            return master;
        }

        // метод регистрации организации
        public Organization RegisterOrganization(OrganizationRequestModel org)
        {
            var hashedPassword = hasher.Generate(org.Password);
            countOrganizationId += 1;
            Organization organization = Organization.Create(countOrganizationId, org.Name, org.TypeId, org.SphereId, org.Phone, org.Address, org.WebAddress, org.Email, hashedPassword);
            db.Organizations.Add(organization);
            db.SaveChanges();
            return organization;
        }

        // метод login 
        public string Login(string password, IUser user)
        {
            var result = hasher.Verify(password, user.Password);
            if (result == false)
            {
                throw new Exception("Ошибка входа. Проверьте пароль.");
            }
            
            var token = jwt.GenerateToken(user);

            return token;
        }
    }
}
