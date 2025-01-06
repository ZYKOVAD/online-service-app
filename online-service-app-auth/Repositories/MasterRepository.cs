using online_service_app_auth.db_layer;
using online_service_app_auth.Services;

namespace online_service_app_auth.Repositories
{
    public class MasterRepository
    {
        private readonly OnlineServiceDbContext _db;
        private readonly PasswordHasher _hasher;
        private static int _countId;

        public MasterRepository(OnlineServiceDbContext db, PasswordHasher hasher)
        {
            _db = db;
            _hasher = hasher;
            _countId = _db.Masters.Max(c => c.Id);
        }

        public Master GetByEmail(string email)
        {
            Master? master = _db.Masters.SingleOrDefault(m => m.Email == email);
            if (master == null) throw new Exception("Мастер не найден");
            return master;
        }
        public Master Create(string name, string surname, string? patronymic, string? phone, string email, string password, int specializationId, int organizationId)
        {
            Master? target = _db.Masters.FirstOrDefault(c => c.Email == email);
            if (target != null) throw new Exception("Мастер с таким email уже существует");
            _countId += 1;
            string hashedPassword = _hasher.Generate(password);
            Master master = new Master(_countId, name, surname, patronymic, phone, email, hashedPassword, specializationId, organizationId);
            _db.Masters.Add(master);
            _db.SaveChanges();
            return master;
        }

    }
}
