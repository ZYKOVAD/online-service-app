using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceService _serviceService;
        public ServiceController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        //получение услуги по id
        [HttpGet]
        public IResult Get(int id)
        {
            try
            {
                Service service = _serviceService.Get(id);
                return Results.Json(service);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение списка услуг организации
        [HttpGet]
        public IResult GetAllByOrganization(int organizationId)
        {
            try
            {
                List<Service> services = _serviceService.GetAllByOrganization(organizationId);
                return Results.Json(services);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение списка услуг, которые оказывает конктретный мастер
        [HttpGet]
        public IResult GetAllByMaster(int masterId)
        {
            try
            {
                List<Service> services = _serviceService.GetAllByMaster(masterId);
                return Results.Json(services);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //редактирование списка услуг, которые оказывает данный мастер
        [HttpPut]
        public IResult UpdateServicesByMaster(int masterId, List<int> servicesIds)
        {
            try
            {
                List<Service> services = _serviceService.UpdateServicesByMaster(masterId, servicesIds);
                return Results.Json(services);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //получение списка мастеров, которые оказывают данную услугу

        //редактирование списка мастеров, которые оказывают данную услугу

        //добавление услуги
        [HttpPost]
        public IResult Create(int organizationId, ServiceModel model)
        {
            try
            {
                Service service = _serviceService.Create(organizationId, model);
                return Results.Json(service);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //редактирование услуги 
        [HttpPut]
        public IResult Update(int id, ServiceModel model)
        {
            try
            {
                Service service = _serviceService.Update(id, model);
                return Results.Json(service);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //удаление услуги
        [HttpDelete]
        public IResult Delete(int id)
        {
            try
            {
                bool result = _serviceService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }
    }
}