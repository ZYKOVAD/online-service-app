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
            return _serviceRepository.Get(id);
        }

        public List<Service> GetAllByOrganization(int organizationId)
        {
            return _serviceRepository.GetAllByOrganization(organizationId);
        }

        public List<Service> GetAllByMaster(int masterId)
        {
            return _serviceRepository.GettAllByMaster(masterId);
        }

        public List<Service> UpdateServicesByMaster(int masterId, List<int> serviceIds)
        {
            return _serviceRepository.UpdateServicesByMaster(masterId, serviceIds);
        }

        public Service Create(int organizationId, ServiceModel model)
        {
            return _serviceRepository.Create(organizationId, model);
        }
        public Service Update(int id, ServiceModel model)
        {
            return _serviceRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _serviceRepository.Delete(id);
        }
    }
}
