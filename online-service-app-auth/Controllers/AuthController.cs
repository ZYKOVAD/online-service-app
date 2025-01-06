using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.Postgres;
using online_service_app_auth.db_layer;
using online_service_app_auth.models;
using online_service_app_auth.Services;
using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace online_service_app_auth.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ClientService _clientService;
        private readonly MasterService _masterService;
        private readonly OrganizationService _organizationService;
        public AuthController(ClientService clientService, UserService userService, MasterService masterService, OrganizationService organizationService)
        {
            _userService = userService;
            _clientService = clientService;
            _masterService = masterService;
            _organizationService = organizationService;
        }

        // регистрация клиента
        [HttpPost]
        public IResult RegisterClient(ClientRequestModel newClient)
        {
            try
            {
                Client client = _clientService.Register(newClient.Name, newClient.Surname, newClient.Patronymic, newClient.Phone, newClient.Email, newClient.Password);
                return Results.Json(client);
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
                Client? client = _clientService.GetByEmail(email);
                var token = _userService.Login(password, client);
                return Results.Json(token);
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
                Master master = _masterService.Register(newMaster.Name, newMaster.Surname, newMaster.Patronymic, newMaster.Phone, newMaster.Email, newMaster.Password, newMaster.SpecializationId, newMaster.OrganizationId);
                return Results.Json(master);
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
                Master? master = _masterService.GetByEmail(email);
                var token = _userService.Login(password, master);
                return Results.Json(token);
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
                Organization organization = _organizationService.Register(newOrganization.Name, newOrganization.TypeId, newOrganization.SphereId, newOrganization.Phone, newOrganization.Address, newOrganization.WebAddress, newOrganization.Email, newOrganization.Password);
                return Results.Json(organization);
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
                Organization? organization = _organizationService.GetByEmail(email);
                var token = _userService.Login(password, organization);
                return Results.Json(token);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        }
    }
}