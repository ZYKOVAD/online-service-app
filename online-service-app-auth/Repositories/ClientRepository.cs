using online_service_app_auth.db_layer;
using online_service_app_auth.Services;

namespace online_service_app_auth.repositories
{
    public class ClientRepository
    {
        private readonly OnlineServiceDbContext _db;
        private static int _countId;
        private readonly PasswordHasher _hasher;

        public ClientRepository(OnlineServiceDbContext db, PasswordHasher hasher)
        {
            _db = db;
            _countId = _db.Clients.Max(c => c.Id);
            _hasher = hasher;
        }

        public Client Get(int id)
        {
            Client? client = _db.Clients.SingleOrDefault(c => c.Id == id);
            if (client == null) throw new Exception("Клиент не найден");
            return client;
        }

        public Client GetByEmail(string email)
        {
            Client? client = _db.Clients.SingleOrDefault(c => c.Email == email);
            if (client == null) throw new Exception("Клиент не найден");
            return client;
        }

        public Client Create(string name, string surname, string? patronymic, string? phone, string email, string password)
        {
            Client? target = _db.Clients.FirstOrDefault(c => c.Email == email);
            if (target != null) throw new Exception("Клиент с таким email уже существует");
            _countId += 1;
            string hashedPassword = _hasher.Generate(password);
            Client client = new Client(_countId, name, surname, patronymic, phone, email, hashedPassword);
            _db.Clients.Add(client);
            _db.SaveChanges();
            return client;
        }
    }
}
