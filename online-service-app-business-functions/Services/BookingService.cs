using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;

namespace online_service_app_business_functions.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly MasterRepository _masterRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly WorkdayRepository _workdayRepository;
        public BookingService(BookingRepository bRepos, MasterRepository mRepos, ServiceRepository sRepos, WorkdayRepository _wRepos)
        {
            _bookingRepository = bRepos;
            _masterRepository = mRepos;
            _serviceRepository = sRepos;
            _workdayRepository = _wRepos;
        }

        public Booking Get(int id)
        {
            return _bookingRepository.Get(id);
        }
        public List<Booking> GetBookingsByClient(int clientId)
        {   
            return _bookingRepository.GetByClient(clientId);
        }

        public Booking Create(int organizationId, int clientId, DateTime dateTime, int masterId, int serviceId)
        {
            return _bookingRepository.Create(organizationId, clientId, dateTime, masterId, serviceId);
        }
        public Booking Update(int id, DateTime dateTime, int statusId)
        {
            return _bookingRepository.Update(id, dateTime, statusId);
        }

        public bool Delete(int id)
        {
            return _bookingRepository.Delete(id);
        }

        public Dictionary<TimeOnly, bool> GetAvailableTime(int masterId, int serviceId, DateOnly date)
        {
            Master master = _masterRepository.Get(masterId);
            Service service = _serviceRepository.Get(serviceId);
            Workday workday = _workdayRepository.GetByMasterAndDate(masterId, date);

            List<Booking> bookings = _bookingRepository.GetByMasterAndDate(masterId, date);

            Dictionary<TimeOnly, short> bookingTimes = new Dictionary<TimeOnly, short>();  //создаем список занятого времени - словарь (начало записи, длительность записи)

            foreach (Booking booking in bookings)
            {
                TimeOnly bookingTime = new TimeOnly(booking.DateTime.Hour, booking.DateTime.Minute);
                Service bookingService = _serviceRepository.Get(booking.ServiceId);
                bookingTimes.Add(bookingTime, bookingService.Duration);
            }

            Dictionary<TimeOnly, bool> availableTimes = new Dictionary<TimeOnly, bool>(); //список доступного времени для записи
            TimeOnly time = workday.TimeStart;
            TimeOnly timeEnd = workday.TimeEnd.AddMinutes(-service.Duration);
            while (time.CompareTo(timeEnd) <= 0)  // разбиваем день на промежутки времени по 15 минут
            {
                availableTimes.Add(time, true);
                time = time.AddMinutes(15);
            }

            foreach (var (bTime, duration) in bookingTimes)
            {
                availableTimes[bTime] = false;
                TimeOnly t = bTime;

                if (duration % 15 == 0)
                {
                    int k = (duration / 15);
                    for (int i = 1; i < k; i++)
                    {
                        t = t.AddMinutes(15);
                        availableTimes[t] = false;
                    }
                }
                else
                {
                    int k = (duration / 15)-1;
                    for (int i = 1; i < k; i++)
                    {
                        t = t.AddMinutes(15);
                        availableTimes[t] = false;
                    }
                }
            }
            return availableTimes;
        }

    }
}


