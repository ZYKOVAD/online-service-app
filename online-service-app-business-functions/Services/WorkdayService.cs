using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class WorkdayService
    {

        private readonly WorkdayRepository _workdayRepository;
        public WorkdayService(WorkdayRepository workdayRepository)
        {
            _workdayRepository = workdayRepository;
        }

        public List<Workday> GetAllByMaster(int masterId)
        {
            return _workdayRepository.GetAllByMaster(masterId);
        }

        public Workday GetByMasterAndDate(int masterId, DateOnly date)
        {
            return _workdayRepository.GetByMasterAndDate(masterId, date);
        }

        public Workday Create(int masterId, WorkdayModel model)
        {
            return _workdayRepository.Create(masterId, model);
        }

        public List<Workday> CreateByDefault(int masterId, List<DateOnly> dates)
        {
            return _workdayRepository.CreateByDefault(masterId, dates);
        }

        public Workday Update(int id, WorkdayModel model)
        {
            return _workdayRepository.Update(id, model);
        }

        public bool Delete(int id)
        {
            return _workdayRepository.Delete(id);
        }
    }
}
