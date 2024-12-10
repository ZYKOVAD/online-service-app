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
        public AuthController(OnlineServiceDbContext db, PasswordHasher hasher, UserService userService)
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

        // авторизация клиента
        [HttpPost]
        public IResult LoginClient(string email, string password)
        {
            try
            {
                Client? target = _db.Clients.FirstOrDefault(c => c.Email == email);
                if (target != null)
                {
                    var token = _userService.Login(password, target);
                    return Results.Json(token);
                }
                else
                {
                    return Results.NotFound(new { message = "Клиент с таким email не зарегистрирован." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            
        }

        // регистрация мастера
        [HttpPost]
        public IResult RegisterMaster(MasterRequestModel newMaster)
        {
            try
            {
                Master? target = _db.Masters.FirstOrDefault(c => c.Email == newMaster.Email);
                if (target == null)
                {
                    Master master = _userService.RegisterMaster(newMaster);
                    return Results.Json(master);
                }
                else
                {
                    return Results.NotFound(new { message = "Мастер с таким email уже зарегистрирован." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }

        }

        // авторизация мастера
        [HttpPost]
        public IResult LoginMaster(string email, string password)
        {
            try
            {
                Master? target = _db.Masters.FirstOrDefault(c => c.Email == email);
                if (target != null)
                {
                    var token = _userService.Login(password, target);
                    return Results.Json(token);
                }
                else
                {
                    return Results.NotFound(new { message = "Мастер с таким email не зарегистрирован." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }

        }

        // регистрация организации
        [HttpPost]
        public IResult RegisterOrganization(OrganizationRequestModel newOrganization)
        {
            try
            {
                Organization? target = _db.Organizations.FirstOrDefault(c => c.Email == newOrganization.Email);
                if (target == null)
                {
                    Organization organization = _userService.RegisterOrganization(newOrganization);
                    return Results.Json(organization);
                }
                else
                {
                    return Results.NotFound(new { message = "Организация с таким email уже зарегистрирована." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }

        }

        // авторизация организации

        [HttpPost]
        public IResult LoginOrganization(string email, string password)
        {
            try
            {
                Organization? target = _db.Organizations.FirstOrDefault(c => c.Email == email);
                if (target != null)
                {
                    var token = _userService.Login(password, target);
                    return Results.Json(token);
                }
                else
                {
                    return Results.NotFound(new { message = "Организация с таким email не зарегистрирована." });
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }
    }
}