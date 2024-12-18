using online_service_app_business_functions.db_layer;

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

        public Booking Create(int orgId, int clientId, DateTime dateTime, int masterId, int serviceId)
        {
            _countId += 1;
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
