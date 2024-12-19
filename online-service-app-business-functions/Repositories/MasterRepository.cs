using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Repositories
{
    public class MasterRepository
    {
        private readonly OnlineServiceDbContext _db;
        public MasterRepository(OnlineServiceDbContext db)
        {
            _db = db;
        }   

        public Master Get(int id)
        {
            Master master = _db.Masters.SingleOrDefault(m => m.Id == id);
            if (master == null) throw new Exception("Мастер с таким id не найден");
            else return master;
        }

        public List<Master> GetByOrganization(int organizationId)
        {
            List<Master> masters = _db.Masters.Where(m => m.OrganizationId == organizationId).ToList();
            return masters;
        }

        public Master Update(int id, MasterModel model)
        {
            Master master = _db.Masters.SingleOrDefault(m => m.Id == id);
            if (master == null) throw new Exception("Мастер с таким id не найден");
            else
            {
                master.Name = model.Name;
                master.Surname = model.Surname;
                master.Patronymic = model.Patronymic; 
                master.Phone = model.Phone;
                master.Email = model.Email;
                master.SpecializationId = model.SpecializationId;
                master.OrganizationId = model.OrganizationId;
                _db.SaveChanges();
                return master;
            }
        }

        public bool Delete(int id)
        {
            Master master = _db.Masters.SingleOrDefault(m => m.Id == id);
            if (master == null) throw new Exception("Мастер с таким id не найден");
            else
            {
                _db.Masters.Remove(master);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
