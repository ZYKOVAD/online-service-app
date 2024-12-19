using Microsoft.AspNetCore.Mvc;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.Models;
using online_service_app_business_functions.Services;

namespace online_service_app_business_functions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IResult Get(int id)
        {
            try
            {
                Client client = _clientService.Get(id);
                return Results.Json(client);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        [HttpPut]
        public IResult Update(int id, ClientModel model)
        {
            try
            {
                Client client = _clientService.Update(id, model);
                return Results.Json(client);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public IResult Delete(int id)
        {
            try
            {
                bool result = _clientService.Delete(id);
                return Results.Json(result);
            }
            catch (Exception ex)
            {
                return Results.Json(new { message = ex.Message });
            }
        }
    }
}
