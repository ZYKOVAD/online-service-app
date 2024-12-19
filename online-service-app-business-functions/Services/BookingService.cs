using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Repositories;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace online_service_app_business_functions.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        public BookingService(BookingRepository repository)
        {
            _bookingRepository = repository;
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
        public List<TimeOnly> GetAvailableTime(int masterId, int serviceId, DateOnly date)
        {
            List<TimeOnly> times = new List<TimeOnly>();
            //Master master = db.Masters.FirstOrDefault(m => m.Id == master_id);
            //Service service = db.Services.FirstOrDefault(s => s.Id == service_id);

            return times;
        }

        
    }
}



//if (master == null | service == null)
//{
//    return Results.NotFound(new { message = "Мастер и/или услуга с таким id не найден(-а)." });
//}
//else
//{
//    if (master.Services.Contains(service))
//    {
//        Workday workday = db.Workdays.FirstOrDefault(w => w.MasterId == master_id && w.Date == date);
//        DateTime d = date.ToDateTime(TimeOnly.MinValue);
//        List<Booking> bookings = db.Bookings.Where(b => (b.MasterId == master_id) && (b.DateTime.Date == d)).ToList();
//        List<TimeOnly> availableTimes = new List<TimeOnly>();
//        TimeOnly time = workday.TimeStart;
//        if (workday.BreakStart == null && workday.BreakEnd == null) // если нет перерыва
//        {
//            TimeOnly timeEnd = workday.TimeEnd.AddMinutes(-service.Duration);
//            while (time.CompareTo(timeEnd) <= 0)  // разбиваем день на промежутки времени по 15 минут
//            {
//                availableTimes.Add(time);
//                time = time.AddMinutes(15);
//            }
//        }
                        //else // если есть перерыв
                        //{
                        //    TimeOnly timeEnd1 = workday.!BreakStart.AddMinutes(-service.Duration);
                        //    while (time.CompareTo(workday.BreakStart) <= 0)

//}
//TimeOnly possible_time = workday.TimeStart; //время, которое будет проверяться на возможность добавления в availableTimes
//while (possible_time.CompareTo(workday.TimeEnd) < 0)  //проверка что текущее время possible_time раньше чем конец рабочего дня
//{
//    TimeOnly end_time = possible_time.AddMinutes(service.Duration); //предполагаемый конец процедуры
//    TimeOnly test_time = possible_time; 
//    while (test_time.CompareTo(end_time) < 0)
//    {

//    }
//}
