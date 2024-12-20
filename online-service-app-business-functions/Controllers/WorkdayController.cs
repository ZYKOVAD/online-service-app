using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkdayController : ControllerBase
    {
        private readonly WorkdayService _workdayService;
        public WorkdayController(WorkdayService workdayService)
        {
            _workdayService = workdayService;
        }

        //получение списка рабочих дней мастера
        [HttpGet]
        public IResult GetAllByMaster(int masterId)
        {
            try
            {
                List<Workday> workdays = _workdayService.GetAllByMaster(masterId);
                return Results.Json(workdays);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //получение рабочего дня по мастеру и дате
        [HttpGet]
        public IResult GetByMasterAndDate(int masterId, DateOnly date)
        {
            try
            {
                Workday workday = _workdayService.GetByMasterAndDate(masterId, date);
                return Results.Json(workday);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //создание нового рабочего дня, открытого для записи клиентов
        [HttpPost]
        public IResult Create(int masterId, WorkdayModel model)
        {
            try
            {
                Workday workday = _workdayService.Create(masterId, model);
                return Results.Json(workday);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //создание новых рабочих дней по настройкам по умолчанию, открытых для записи клиентов
        [HttpPost]
        public IResult Create(int masterId, List<DateOnly> dates)
        {
            try
            {
                List<Workday> workdays = _workdayService.CreateByDefault(masterId, dates);
                return Results.Json(workdays);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //редактирование рабочего дня
        [HttpPut]
        public IResult Update(int id, WorkdayModel model)
        {
            try
            {
                Workday workday = _workdayService.Update(id, model);
                return Results.Json(workday);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //удаление рабочего дня
        [HttpDelete]
        public IResult Delete(int id)
        {
            try
            {
                bool result = _workdayService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }
    }
}
