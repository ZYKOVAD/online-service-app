using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;
        public ServiceService(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Service Get(int id)
        {
            Service service = _serviceRepository.Get(id);
            return service;
        }

        public List<Service> GetAllByOrganization(int organizationId)
        {
            List<Service> services = _serviceRepository.GetAllByOrganization(organizationId);
            return services;
        }

        public List<Service> GetAllByMaster(int masterId)
        {
            List<Service> services = _serviceRepository.GettAllByMaster(masterId);
            return services;
        }

        public List<Service> UpdateServicesByMaster(int masterId, List<int> serviceIds)
        {
            List<Service> services = _serviceRepository.UpdateServicesByMaster(masterId, serviceIds);
            return services;
        }

        public Service Create(int organizationId, ServiceModel model)
        {
            Service service = _serviceRepository.Create(organizationId, model);
            return service;
        }
        public Service Update(int id, ServiceModel model)
        {
            Service service = _serviceRepository.Update(id, model);
            return service;
        }

        public bool Delete(int id)
        {
            bool result = _serviceRepository.Delete(id);
            return result;
        }
    }
}
