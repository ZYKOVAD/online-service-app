using Microsoft.AspNetCore.Mvc;

namespace audit_service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditController : ControllerBase
    {
        [HttpGet]
        public IResult GetInfo()
        {
            return Results.Json(Db.Data);
        }
    }
}

