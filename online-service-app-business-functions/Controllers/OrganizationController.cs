using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.db_layer;
using online_service_app_business_functions.Models;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OnlineServiceDbContext db;
        public OrganizationController(OnlineServiceDbContext _db)
        {
            db = _db;
        }

        //получение списка организаций, в которых у клиента до этого была запись
        [Authorize]
        [HttpGet]
        public IResult GetOrganizationsByClient(int client_id)
        {
            List<Service> services = db.Services.Where(c => c.Id == client_id).ToList();
            List<Organization> organizations = new List<Organization>();
            foreach (Service service in services)
            {
                if (!(organizations.Contains(service.Organization))) {
                    organizations.Add(service.Organization);
                }
            }
            return Results.Json(organizations);
        }

        //получение информации об организации по id
        [Authorize]
        [HttpGet]
        public IResult GetOrganization(int id)
        {
            Organization organization = db.Organizations.FirstOrDefault(o => o.Id == id);
            return Results.Json(organization);
        }

        //поиск организации по названию (подстроке)

        //изменение информации о организации
        [Authorize(Policy = "OnlyForOrganization")]
        [HttpPut]
        public IResult UpdateOrganization(int org_id, OrganizationModel new_inf)
        {
            try
            {
                Organization organization = db.Organizations.FirstOrDefault(o => o.Id == org_id);
                if (organization != null)
                {
                    organization.Name = new_inf.Name;
                    organization.TypeId = new_inf.TypeId;
                    organization.SphereId = new_inf.SphereId;
                    organization.Phone = new_inf.Phone;
                    organization.Address = new_inf.Address;
                    organization.WebAddress = new_inf.WebAddress;
                    organization.Email = new_inf.Email;
                    
                    return Results.Json(organization);
                }
                else
                {
                    return Results.NotFound(new { message = "Организация не найдена" });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            

        }

        //работа с типами организаций и сферами деятельности?

    }
}
