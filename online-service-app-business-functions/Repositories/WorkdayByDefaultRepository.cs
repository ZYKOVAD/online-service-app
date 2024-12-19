using online_service_app_business_functions.Models;
using online_service_app_business_functions.DbLayer;

namespace online_service_app_business_functions.Repositories
{
    public class WorkdayByDefaultRepository
    {
        private readonly OnlineServiceDbContext _db;
        private static int countId;
        public WorkdayByDefaultRepository(OnlineServiceDbContext db)
        {
            _db = db;
            countId = _db.WorkdayByDefaults.Max(w =>  w.Id);
        }

        public WorkdayByDefault Get(int id)
        {
            WorkdayByDefault workdayByDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.Id == id);
            if (workdayByDefault == null) throw new Exception("Не найдено");
            else return workdayByDefault;
        }

        public WorkdayByDefault GetByMaster(int masterId)
        {
            WorkdayByDefault workdayByDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.MasterId == masterId);
            if (workdayByDefault == null) throw new Exception("Не найдено");
            else return workdayByDefault; 
        }

        public WorkdayByDefault Create(int masterId, WorkdayByDefaultModel model)
        {
            countId += 1;
            WorkdayByDefault workdayByDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.MasterId==masterId);
            if (workdayByDefault == null)
            {
                workdayByDefault = new WorkdayByDefault(countId, masterId, model.TimeStart, model.TimeEnd, model.BreakStart, model.BreakEnd);
                _db.WorkdayByDefaults.Add(workdayByDefault);
                _db.SaveChanges();
                return workdayByDefault;
            }
            else throw new Exception("Настройки дня по умолчанию уже существуют");
        }

        public WorkdayByDefault Update(int id, WorkdayByDefaultModel model)
        {
            WorkdayByDefault workdayByDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.Id == id);
            if (workdayByDefault == null) throw new Exception("Не найдено");
            else
            {
                workdayByDefault.TimeStart = model.TimeStart;
                workdayByDefault.TimeEnd = model.TimeEnd;
                workdayByDefault.BreakStart = model.BreakStart;
                workdayByDefault.BreakEnd = model.BreakEnd;
                _db.SaveChanges();
                return workdayByDefault;
            }
        }

        public bool Delete(int id)
        {
            WorkdayByDefault workdayByDefault = _db.WorkdayByDefaults.SingleOrDefault(w => w.Id == id);
            if (workdayByDefault == null) throw new Exception("Не найдено");
            else
            {
                _db.WorkdayByDefaults.Remove(workdayByDefault);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
