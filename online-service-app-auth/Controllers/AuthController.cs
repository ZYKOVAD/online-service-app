using Microsoft.AspNetCore.Mvc;
using online_service_app_auth.db_layer;
using online_service_app_auth.models;

namespace online_service_app_auth.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly OnlineServiceDbContext _db;
        private readonly PasswordHasher _hasher;
        private readonly UserService _userService;
        public   AuthController(OnlineServiceDbContext db, PasswordHasher hasher, UserService userService)
        {
            _db = db;
            _hasher = hasher;
            _userService = userService;
        }

        // регистрация клиента
        [HttpPost]
        public IResult RegisterClient(ClientRequestModel newClient)
        {
            try
            {
                Client? target = _db.Clients.FirstOrDefault(c => c.Email == newClient.Email);
                if (target == null)
                {
                    Client client = _userService.RegisterClient(newClient);
                    return Results.Json(client);
                }
                else
                {
                    return Results.NotFound(new { message = "Клиент с таким email уже зарегистрирован." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            
        }

        // метод для авторизации клиента

        // метод для авторизации мастера

        // метод для авторизации организации

    }
}