

using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Repositories
{
    public class ServiceRepository
    {
        private readonly OnlineServiceDbContext _db;
        private static int _countId;
        public ServiceRepository(OnlineServiceDbContext db)
        {
            _db = db;
            _countId = _db.Services.Max(x => x.Id);
        }

        public Service Get(int id)
        {
            Service service = _db.Services.SingleOrDefault(s => s.Id == id);
            if (service == null) throw new Exception("Услуга не найдена");
            return service;
        }

        public List<Service> GetAllByOrganization(int organizationId)
        {
            Organization organization = _db.Organizations.SingleOrDefault(o => o.Id == organizationId);
            if (organization == null) throw new Exception("Организация не найдена");
            List<Service> services = _db.Services.Where(s => s.OrganizationId == organizationId).ToList();
            return services;
        }

        public List<Service> GettAllByMaster(int masterId)
        {
            Master master = _db.Masters.SingleOrDefault(m => m.Id == masterId);
            if (master == null) throw new Exception("Мастер не найден");
            List<Service> services = master.Services.ToList();
            return services;
        }

        public List<Service> UpdateServicesByMaster(int masterId, List<int> serviceIds)
        {
            Master master = _db.Masters.SingleOrDefault(m => m.Id == masterId);
            if (master == null) throw new Exception("Мастер не найден");
            List<Service> newServices = new List<Service>();
            master.Services = newServices;

            foreach (int serviceId in serviceIds)
            {
                Service service = _db.Services.SingleOrDefault(s => s.Id == serviceId);
                if (service == null) throw new Exception("Услуга не найдена");
                master.Services.Add(service);
            }
            _db.SaveChanges();
            return newServices;
        }

        public Service Create(int organizationId, ServiceModel model)
        {
            _countId += 1;
            Service newService = new Service(_countId, model.Name, organizationId, model.Duration, model.Price, model.Description);
            _db.Services.Add(newService);
            _db.SaveChanges();
            return newService;
        }

        public Service Update(int id, ServiceModel model)
        {
            Service service = _db.Services.SingleOrDefault(s => s.Id == id);
            if (service == null) throw new Exception("Услуга не найдена");
            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;
            service.Duration = model.Duration;
            _db.SaveChanges();
            return service;
        }

        public bool Delete(int id)
        {
            Service service = _db.Services.SingleOrDefault(s => s.Id == id);
            if (service == null) throw new Exception("Услуга не найдена");
            _db.Remove(service);
            _db.SaveChanges();
            return true;
        }
    }
}
