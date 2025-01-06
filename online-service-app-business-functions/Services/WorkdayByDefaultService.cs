using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class WorkdayByDefaultService
    {
        private readonly WorkdayByDefaultRepository _workdayByDefaultRepository;
        public WorkdayByDefaultService(WorkdayByDefaultRepository workdayByDefaultRepository)
        {
            _workdayByDefaultRepository = workdayByDefaultRepository;
        }

        public WorkdayByDefault Get(int id)
        {
            WorkdayByDefault workdayByDefault = _workdayByDefaultRepository.Get(id);
            return workdayByDefault;
        }

        public WorkdayByDefault GetByMaster(int masterId)
        {
            WorkdayByDefault workdayByDefault = _workdayByDefaultRepository.GetByMaster(masterId);
            return workdayByDefault;
        }

        public WorkdayByDefault Create(int masterId, WorkdayByDefaultModel model)
        {
            WorkdayByDefault workdayByDefault = _workdayByDefaultRepository.Create(masterId, model);
            return workdayByDefault;
        }
        public WorkdayByDefault Update(int id, WorkdayByDefaultModel model)
        {
            WorkdayByDefault workdayByDefault = _workdayByDefaultRepository.Update(id, model);
            return workdayByDefault;
        }

        public bool Delete(int id)
        {
            bool result = _workdayByDefaultRepository.Delete(id);
            return result;
        }
    }
}
