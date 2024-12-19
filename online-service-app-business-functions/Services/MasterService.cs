using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class MasterService
    {
        private readonly MasterRepository _masterRepository;
        public MasterService(MasterRepository repos)
        {
            _masterRepository = repos;
        }

        public Master Get(int id)
        {
            return _masterRepository.Get(id);
        }
        public List<Master> GetByOrganization(int organizationId)
        {
            return _masterRepository.GetByOrganization(organizationId);
        }

        public Master Update(int id, MasterModel model)
        {
            return _masterRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _masterRepository.Delete(id);
        }
    }
}