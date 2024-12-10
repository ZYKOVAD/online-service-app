using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using System.Net;

namespace online_service_app_auth
{
    public class UserService
    {
        private readonly PasswordHasher hasher;
        private readonly OnlineServiceDbContext db;
        private static int countId;
        public UserService(PasswordHasher _hasher, OnlineServiceDbContext _db)
        {
            hasher = _hasher;
            db = _db;
            countId = db.Clients.Max(c => c.Id);
        }

        // метод регистрации клиента
        public Client RegisterClient(ClientRequestModel cl)
        {
            var hashedPassword = hasher.Generate(cl.Password);
            countId += 1;
            Client client = Client.Create(countId, cl.Name, cl.Surname, cl.Patronymic, cl.Phone, cl.Email, hashedPassword);
            db.Clients.Add(client);
            db.SaveChanges();
            return client;   
        }

        // метод login клиента
    }
}
