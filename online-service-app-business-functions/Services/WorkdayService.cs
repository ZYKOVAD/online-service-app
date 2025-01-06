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
            List<Workday> workdays = _workdayRepository.GetAllByMaster(masterId);
            return workdays;
        }

        public Workday GetByMasterAndDate(int masterId, DateOnly date)
        {
            Workday workday = _workdayRepository.GetByMasterAndDate(masterId, date);
            return workday;
        }

        public Workday Create(int masterId, WorkdayModel model)
        {
            Workday workday = _workdayRepository.Create(masterId, model);
            return workday;
        }

        public List<Workday> CreateByDefault(int masterId, List<DateOnly> dates)
        {
            List<Workday> workdays = _workdayRepository.CreateByDefault(masterId, dates);
            return workdays;
        }

        public Workday Update(int id, WorkdayModel model)
        {
            Workday workday = _workdayRepository.Update(id, model);
            return workday;
        }

        public bool Delete(int id)
        {
            bool result = _workdayRepository.Delete(id);
            return result;
        }
    }
}
