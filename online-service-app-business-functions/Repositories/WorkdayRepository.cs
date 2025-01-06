using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Repositories
{
    public class WorkdayRepository
    {
        private readonly OnlineServiceDbContext _db;
        private int countId; 
        
        public WorkdayRepository(OnlineServiceDbContext db)
        {
            _db = db;
            countId = _db.Workdays.Max(x => x.Id);
        }

        public List<Workday> GetAllByMaster(int masterId)
        {
            List<Workday> workdays = _db.Workdays.Where(w => w.MasterId == masterId).ToList();
            return workdays;
        }

        public Workday GetByMasterAndDate(int masterId, DateOnly date)
        {
            Workday? workday = _db.Workdays.SingleOrDefault(w => w.MasterId == masterId && w.Date == date);
            if (workday == null) throw new Exception("Рабочий день не найден");
            return workday;
        }

        public Workday Create(int masterId, WorkdayModel model)
        {
            countId += 1; ;
            Workday newWorkday = new Workday(countId, masterId, model.Date, model.TimeStart, model.TimeEnd, model.BreakStart, model.BreakEnd);
            _db.Workdays.Add(newWorkday);
            _db.SaveChanges();
            return newWorkday;
        }

        public List<Workday> CreateByDefault(int masterId, List<DateOnly> dates)
        {
            WorkdayByDefault? byDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.MasterId == masterId);
            if (byDefault == null) throw new Exception("Настройки дня по умолчанию отсутствуют");
            foreach (var date in dates)
            {
                countId += 1;
                Workday newWorkday = new Workday(countId, masterId, date, byDefault.TimeStart, byDefault.TimeEnd, byDefault.BreakStart, byDefault.BreakEnd);
                _db.Workdays.Add(newWorkday);
            }
            _db.SaveChanges();
            List<Workday> workdays = _db.Workdays.Where(w => w.MasterId == masterId).ToList();
            return workdays;
        }

        public Workday Update(int id, WorkdayModel model)
        {
            Workday? workday = _db.Workdays.SingleOrDefault(w => w.Id == id);
            if (workday == null) throw new Exception("Рабочий день отсутствует");
            //проверка на наличие записей
            DateTime date = new DateTime(workday.Date, TimeOnly.MinValue);
            List<Booking> bookings = _db.Bookings.Where(b => b.DateTime.Date == date).ToList();
            if (bookings != null) throw new Exception("Невозможно обновить рабочий день, так как на эту дату уже сделана запись");

            workday.TimeStart = model.TimeStart;
            workday.TimeEnd = model.TimeEnd;
            workday.BreakStart = model.BreakStart;
            workday.BreakEnd = model.BreakEnd;
            _db.SaveChanges();
            return workday;
        }

        public bool Delete(int id)
        {
            Workday? workday = _db.Workdays.SingleOrDefault(w => w.Id == id);
            if (workday == null) throw new Exception("Рабочий день отсутствует");
            //проверка на наличие записей
            DateTime date = new DateTime(workday.Date, TimeOnly.MinValue);
            List<Booking> bookings = _db.Bookings.Where(b => b.DateTime.Date == date).ToList();
            if (bookings != null) throw new Exception("Невозможно удалить рабочий день, так как на эту дату уже сделана запись");
            
            _db.Workdays.Remove(workday);
            _db.SaveChanges();
            return true;
        }
    }
}
