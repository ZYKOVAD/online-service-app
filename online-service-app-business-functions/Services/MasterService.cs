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
            Master master = _masterRepository.Get(id);
            return master;
        }
        public List<Master> GetByOrganization(int organizationId)
        {
            List<Master> masters = _masterRepository.GetByOrganization(organizationId);
            return masters;
        }

        public Master Update(int id, MasterModel model)
        {
            Master master = _masterRepository.Update(id, model);
            return master;
        }

        public bool Delete(int id)
        {
            bool result = _masterRepository.Delete(id);
            return result;
        }
    }
}