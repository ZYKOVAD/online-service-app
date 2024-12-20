using online_service_app_business_functions.DbLayer;

namespace online_service_app_business_functions.Repositories
{
    public class BookingRepository 
    {
        
        private readonly OnlineServiceDbContext _db;
        private static int _countId;
        public BookingRepository(OnlineServiceDbContext db)
        {
            _db = db;
            _countId = _db.Bookings.Max(b => b.Id);
        }

        public Booking Get(int id)
        {
            return _db.Bookings.SingleOrDefault(b => b.Id == id);
        }

        public List<Booking> GetByClient(int clientId)
        {
            List<Booking> bookings = _db.Bookings.Where(b => b.ClientId == clientId).ToList();
            return bookings;
        }

        public List<Booking> GetByMasterAndDate(int masterId, DateOnly date)
        {
            DateTime dateTime = new DateTime(date, TimeOnly.MinValue);
            List<Booking> bookings = _db.Bookings.Where(b => b.MasterId == masterId && b.DateTime.Date == dateTime).ToList();
            return bookings;
        }

        public Booking Create(int orgId, int clientId, DateTime dateTime, int masterId, int serviceId)
        {
            _countId += 1;
            Master master = _db.Masters.SingleOrDefault(m => m.Id == masterId);
            Organization organization = _db.Organizations.SingleOrDefault(p => p.Id == orgId);
            Service service = _db.Services.SingleOrDefault(s => s.Id == serviceId);
            List<Master> masters = service.Masters.ToList();
            bool isMasterService = false;
            if (organization == null) throw new Exception("Организация не найдена");
            if (master == null) throw new Exception("Мастер не найден");
            if (service == null) throw new Exception("Услуга не найдена");
            if (master.OrganizationId != orgId) throw new Exception("Мастер не привязан к этой организации");
            foreach (var m in masters)
            {
                if (m.Id == masterId) isMasterService = true;
            }
            if (!isMasterService) throw new Exception("Мастер не оказывает данную услугу");


            Booking newBooking = new Booking(_countId, orgId, clientId, dateTime, masterId, serviceId);
            _db.Bookings.Add(newBooking);
            _db.SaveChanges();
            return newBooking;
        }

        public Booking Update(int id, DateTime dateTime, int statusId)
        {
            Booking booking = _db.Bookings.SingleOrDefault(b => b.Id == id);
            booking.DateTime = dateTime;
            booking.StatusId = statusId;
            _db.SaveChanges();
            return booking;
        }

        public bool Delete(int id)
        {
            Booking booking = _db.Bookings.SingleOrDefault(b => b.Id == id);
            if (booking == null) throw new Exception("Запись не найдена");
            _db.Bookings.Remove(booking);
            _db.SaveChanges();
            return true;
        }
    }
}
