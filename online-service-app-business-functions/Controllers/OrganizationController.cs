using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _organizationService;
        public OrganizationController(OrganizationService service)
        {
            _organizationService = service;
        }

        // получение списка id организаций, в которых у клиента до этого была запись
        //[Authorize(Policy = "Default")]
        [HttpGet]
        public IResult GetOrganizationIdsByClient(int clientId)
        {
            try
            {
                List<int> organizationIds = _organizationService.GetOrganizationIdsByClient(clientId);
                return Results.Json(organizationIds);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }

        }

        //получение информации об организации по id
        //[Authorize(Policy = "OnlyForClients")]
        [HttpGet]
        public IResult Get(int id)
        {
            try
            {
                Organization organization = _organizationService.Get(id);
                return Results.Json(organization);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IResult GetAll()
        {
            try
            {
                List<Organization> organizations = _organizationService.GetAll();
                return Results.Json(organizations);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        //todo поиск организации по названию (подстроке)

        //изменение информации об организации
        //[Authorize(Policy = "OnlyForOrganization")]
        [HttpPut]
        public IResult Update(int orgId, OrganizationModel model)
        {
            try
            {
                Organization organization = _organizationService.Update(orgId, model);
                return Results.Json(organization);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }

        //удаление организации
        [HttpDelete]
        public IResult Delete(int orgId)
        {
            try
            {
                bool result = _organizationService.Delete(orgId);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }
    }
}
