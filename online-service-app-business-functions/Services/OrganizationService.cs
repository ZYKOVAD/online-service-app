using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class OrganizationService
    {
        private readonly OrganizationRepository _organizationRepository;
        public OrganizationService(OrganizationRepository repos)
        {
            _organizationRepository = repos;
        } 

        public Organization Get(int id)
        {
            Organization organization = _organizationRepository.Get(id);
            return organization;
        }

        public List<Organization> GetAll()
        {
            List<Organization> organizations = _organizationRepository.GetAll();
            return organizations;
        }

        public List<int> GetOrganizationIdsByClient(int clientId)
        {
            List<int> organizationIds = _organizationRepository.GetOrganizationIdsByClient(clientId);
            return organizationIds;
        }

        public Organization Update(int id, OrganizationModel model)
        {
            Organization organization = _organizationRepository.Update(id, model);
            return organization;
        }

        public bool Delete(int id)
        {
            bool result = _organizationRepository.Delete(id);
            return result;
        }
    }
}
