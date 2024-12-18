using online_service_app_business_functions.db_layer;
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

        public List<Organization> GetOrganizationsByClient(int clientId)
        {
            List<Booking> bookings = _db.Bookings.Where(b => b.ClientId == clientId).ToList();
            List<int> orgIds = new List<int>();
            foreach (Booking booking in bookings)
            {
                orgIds.Add(booking.OrganizationId);
            }
            List<Organization> organizations = new List<Organization>();
            foreach (int Id in orgIds)
            {
                Organization organization = _db.Organizations.SingleOrDefault(o => o.Id == Id);
                organizations.Add(organization);
            }
            return organizations;
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
