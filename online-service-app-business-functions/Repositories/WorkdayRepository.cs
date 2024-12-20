using online_service_app_business_functions.DbLayer;

namespace online_service_app_business_functions.Repositories
{
    public class WorkdayRepository
    {
        private readonly OnlineServiceDbContext _db;
        
        public WorkdayRepository(OnlineServiceDbContext db)
        {
            _db = db;
        }

        public List<Workday> GetAllByMaster(int masterId)
        {
            List<Workday> workdays = _db.Workdays.Where(w => w.MasterId == masterId).ToList();
            return workdays;
        }

        public Workday GetByMasterAndDate(int masterId, DateOnly date)
        {
            Workday workday = _db.Workdays.SingleOrDefault(w => w.MasterId == masterId && w.Date == date);
            if (workday == null) throw new Exception("Рабочий день не найден");
            return workday;
        }
    }
}
