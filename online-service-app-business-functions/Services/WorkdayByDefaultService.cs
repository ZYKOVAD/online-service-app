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
            return _workdayByDefaultRepository.Get(id);
        }

        public WorkdayByDefault GetByMaster(int masterId)
        {
            return _workdayByDefaultRepository.GetByMaster(masterId);
        }

        public WorkdayByDefault Create(int masterId, WorkdayByDefaultModel model)
        {
            return _workdayByDefaultRepository.Create(masterId, model);
        }
        public WorkdayByDefault Update(int id, WorkdayByDefaultModel model)
        {
            return _workdayByDefaultRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _workdayByDefaultRepository.Delete(id);
        }
    }
}
