using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkdayByDefaultController : ControllerBase
    {
        private readonly WorkdayByDefaultService _workdayByDefaultService;

        public WorkdayByDefaultController(WorkdayByDefaultService service)
        {
            _workdayByDefaultService = service;
        }

        //получение рабочего дня мастера по умолчанию (настроек) по id
        [HttpGet]
        public IResult Get(int id)
        {
            try
            {
                WorkdayByDefault workdayByDefault = _workdayByDefaultService.Get(id);
                return Results.Json(workdayByDefault);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение рабочего дня мастера по умолчанию (настроек) по id мастера
        [HttpGet]
        public IResult GetByMaster(int masterId)
        {
            try
            {
                WorkdayByDefault workdayByDefault = _workdayByDefaultService.GetByMaster(masterId);
                return Results.Json(workdayByDefault);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //создание рабочего дня мастера по умолчанию (настроек) 
        [HttpPost]
        public IResult Create(int masterId, WorkdayByDefaultModel model)
        {
            try
            {
                WorkdayByDefault workdayByDefault = _workdayByDefaultService.Create(masterId, model);
                return Results.Json(workdayByDefault);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //редактирование рабочего дня мастера по умолчанию (настроек) 
        [HttpPut]
        public IResult Update(int id, WorkdayByDefaultModel model)
        {
            try
            {
                WorkdayByDefault workdayByDefault = _workdayByDefaultService.Update(id, model);
                return Results.Json(workdayByDefault);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //удаление рабочего дня мастера по умолчанию (настроек) 
        [HttpDelete]
        public IResult Delete(int id)
        {
            try
            {
                bool result = _workdayByDefaultService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }
    }
}
