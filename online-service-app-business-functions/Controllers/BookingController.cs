using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.db_layer;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly OnlineServiceDbContext db;
        public BookingController(OnlineServiceDbContext _db)
        {
            db = _db;
        }

        //получение списка предыдущих записей клиента
        [HttpGet]
        public IResult GetBookingsByClient(int client_id)
        {
            try
            {
                List<Booking> bookings = db.Bookings.Where(b => b.ClientId == client_id).ToList();
                return Results.Json(bookings);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }

        }

        //получение информации о записи по id
        [HttpGet]
        public IResult GetBooking(int booking_id)
        {
            try
            {
                Booking booking = db.Bookings.FirstOrDefault(b => b.Id == booking_id);
                return Results.Json(booking);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение доступного времени мастера для записи (окошек)
        [HttpGet]
        public IResult GetAvailableTime(int master_id, int service_id, DateOnly date)
        {
            try
            {
                Master master = db.Masters.FirstOrDefault(m => m.Id == master_id);
                Service service = db.Services.FirstOrDefault(s => s.Id == service_id);
                if (master == null | service == null)
                {
                    return Results.NotFound(new { message = "Мастер и/или услуга с таким id не найден(-а)." });
                }
                else
                {
                    if (master.Services.Contains(service))
                    {
                        Workday workday = db.Workdays.FirstOrDefault(w => w.MasterId == master_id && w.Date == date);
                        DateTime d = date.ToDateTime(TimeOnly.MinValue);
                        List<Booking> bookings = db.Bookings.Where(b => (b.MasterId == master_id) && (b.DateTime.Date == d)).ToList();
                        List<TimeOnly> availableTimes = new List<TimeOnly>();
                        TimeOnly time = workday.TimeStart;
                        if (workday.BreakStart == null && workday.BreakEnd == null) // если нет перерыва
                        {
                            TimeOnly timeEnd = workday.TimeEnd.AddMinutes(-service.Duration);
                            while (time.CompareTo(timeEnd) <= 0)  // разбиваем день на промежутки времени по 15 минут
                            {
                                availableTimes.Add(time);
                                time = time.AddMinutes(15);
                            }
                        }
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
                        return Results.Json(availableTimes);
                    }
                    else
                    {
                        return Results.NotFound(new { message = $"Мастер с id {master_id} не оказывает услугу с id {service_id}." });
                    }
                }

            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //создание записи

        //удаление записи (отмена)

        //перенос записи

        //работа со статусами? (создание, удаление, редактирование)

    }
}