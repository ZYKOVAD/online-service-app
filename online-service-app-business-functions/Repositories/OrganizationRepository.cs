using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Repositories
{
    public class OrganizationRepository
    {
        private readonly OnlineServiceDbContext _db;
        public OrganizationRepository(OnlineServiceDbContext db)
        {
            _db = db;
        }

        public Organization Get(int id)
        {
            Organization organization = _db.Organizations.SingleOrDefault(o => o.Id == id);
            if (organization == null) throw new Exception("Организация с таким id не найдена");
            else return organization;
        }

        public List<Organization> GetAll()
        {
            return _db.Organizations.ToList();
        }

        public List<int> GetOrganizationIdsByClient(int clientId)
        {
            List<Booking> bookings = _db.Bookings.Where(b => b.ClientId == clientId).ToList();
            List<int> orgIds = new List<int>();
            List<Organization> organizations = new List<Organization>();
            foreach (Booking booking in bookings)
            {
                orgIds.Add(booking.OrganizationId);
            }
            return orgIds;
        }

        public Organization Update(int id, OrganizationModel model)
        {
            Organization organization = _db.Organizations.SingleOrDefault(o => o.Id == id);
            if (organization == null) throw new Exception("Организация с таким id не найдена");
            else
            {
                organization.Name = model.Name;
                organization.TypeId = model.TypeId;
                organization.SphereId = model.SphereId;
                organization.Phone = model.Phone;
                organization.Address = model.Address;
                organization.WebAddress = model.WebAddress;
                organization.Email = model.Email;
                return organization;
            }
        }

        public bool Delete(int id)
        {
            Organization organization = _db.Organizations.SingleOrDefault(o => o.Id == id);
            if (organization == null) throw new Exception("Организация с таким id не найдена");
            else
            {
                _db.Organizations.Remove(organization);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
