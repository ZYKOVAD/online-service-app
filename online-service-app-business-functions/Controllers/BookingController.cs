using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.db_layer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.RabbitMQ;
using online_service_app_business_functions.Servises;
using System.Diagnostics;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly RabbitMqService _rabbit;
        public BookingController(RabbitMqService rabbit, BookingService bookingService)
        {
            _rabbit = rabbit;
            _bookingService = bookingService;
        }

        //получение списка предыдущих записей клиента
        //[Authorize(Policy = "Default")]
        [HttpGet]
        public IResult GetBookingsByClient(int clientId)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                List<Booking> bookings = _bookingService.GetBookingsByClient(clientId);

                stopwatch.Stop();

                //RabbitMqModelMessage message = new RabbitMqModelMessage("GetBookingsByClient", stopwatch.ElapsedMilliseconds);
                //_rabbit.SendMessage(message);

                return Results.Json(bookings);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }

        }

        //получение информации о записи по id
        [HttpGet]
        public IResult GetBooking(int bookingId)
        {
            try
            {
                Booking booking = _bookingService.Get(bookingId);
                return Results.Json(booking);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение доступного времени мастера для записи (окошек) - не дописан
        [HttpGet]
        public IResult GetAvailableTime(int masterId, int serviceId, DateOnly date)
        {
            try
            {
                List<TimeOnly> times = _bookingService.GetAvailableTime(masterId, serviceId, date);
                return Results.Json(times);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //создание записи
        [HttpPost]
        public IResult CreateBooking(int organizationId, int clientId, DateTime dateTime, int masterId, int serviceId)
        {
            try
            {
                Booking booking = _bookingService.Create(organizationId, clientId, dateTime, masterId, serviceId);
                return Results.Json(booking);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //перенос записи
        [HttpPut]
        public async Task<IResult> UpdateBooking(int id, DateTime dateTime, int statusId)
        {
            try
            {
                Booking booking = _bookingService.Update(id, dateTime, statusId);
                return Results.Json(booking);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //удаление записи (отмена)
        [HttpDelete]
        public IResult DeleteBooking(int id)
        {
            try
            {
                bool result = _bookingService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //работа со статусами? (создание, удаление, редактирование)

    }
}