using online_service_app_auth.db_layer;
using online_service_app_auth.Services;
using System.Numerics;
using System.Xml.Linq;

namespace online_service_app_auth.Repositories
{
    public class OrganizationRepository
    {
        private readonly OnlineServiceDbContext _db;
        private readonly PasswordHasher _hasher;
        private static int _countId;
        public OrganizationRepository(OnlineServiceDbContext db, PasswordHasher hasher)
        {
            _db = db;
            _hasher = hasher;
            _countId = _db.Organizations.Max(c => c.Id);
        }

        public Organization GetByEmail(string email)
        {
            Organization? organization = _db.Organizations.SingleOrDefault(o => o.Email == email);
            if (organization == null) throw new Exception("Организация не найдена");
            return organization;
        }

        public Organization Create(string name, int typeId, int? shereId, string? phone, string? address, string? webAddress, string email, string password)
        {
            Organization? target = _db.Organizations.FirstOrDefault(o => o.Email == email);
            if (target != null) throw new Exception("Организация с таким email уже существует");
            _countId += 1;
            string hashedPassword = _hasher.Generate(password);
            Organization organization = new Organization(_countId, name, typeId, shereId, phone, address, webAddress, email, hashedPassword);
            _db.Organizations.Add(organization);
            _db.SaveChanges();
            return organization;
        }
    }
}
