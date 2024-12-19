using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MasterController : ControllerBase
    {
        private readonly MasterService _masterService;

        public MasterController(MasterService masterService)
        {
            _masterService = masterService;
        }

        //получение списка мастеров организации
        [HttpGet]
        public IResult GetAllByOrganization(int organizationId)
        {
            try
            {
                List<Master> masters = _masterService.GetByOrganization(organizationId);
                return Results.Json(masters);
            }
            catch(Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение информации о мастере по id
        [HttpGet]
        public IResult Get(int id)
        {
            try
            {
                Master master = _masterService.Get(id);
                return Results.Json(master);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }

        }

        //редактирование информации о мастере 
        [HttpPut]
        public IResult Update(int id, MasterModel model)
        {
            try
            {
                Master master = _masterService.Update(id, model);
                return Results.Json(master);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //удаление мастера
        [HttpDelete]
        public IResult Delete(int id)
        {
            try
            {
                bool result = _masterService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

    }
}