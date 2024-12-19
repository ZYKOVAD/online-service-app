using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using online_service_app_business_functions.DbLayer;
using online_service_app_business_functions.RabbitMQ;
using online_service_app_business_functions.Repositories;
using online_service_app_business_functions.Services;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("JwtBearerDefaults.AuthenticationScheme", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });


    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "bearerAuth"
    //            }
    //        },
    //        new string[] {}
    //    }
    //});
});

builder.Services.AddScoped<OnlineServiceDbContext>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<OrganizationRepository>();
builder.Services.AddScoped<MasterService>();
builder.Services.AddScoped<MasterRepository>();
builder.Services.AddScoped<WorkdayByDefaultService>();
builder.Services.AddScoped<WorkdayByDefaultRepository>();
builder.Services.AddScoped<RabbitMqService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var publicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAm+6A0cvML3dDEDNvWu6H
nmSHu5TVX+mdLhLkL3juu1RLjsFGefrNvhmVEqwhp2m2oDUYG2y0RoFsMPuEfguU
fqPq/Tgiu7cHcRQSXEhBV+WufNgFMbeUf5K90MHjnGygsbUuZoxLH8dQqOft+ZYm
ynhk0rKbumRONlC9G9qTRes0bG4TXRT1H/+QBeYWK71OzSM4pKwr9Z+1FTc7ZIYs
lExWVy0w5tZdN4v2sUWHoQhK9DgZGdHgnxLYsdIfNoXi6TMVqLyGfh0B5hDIfX0h
PZPfMesVGuOwYXUDFJakl3sjfH9COEUiTALA8YpAeh+HWqkdTCTea4mFnkNaFneY
iQIDAQAB
-----END PUBLIC KEY-----";

    using var rsa = RSA.Create();
    rsa.ImportFromPem(publicKey);

    // устанавливаем параметры для валидации токена
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // указываем ключ для проверки подписи
        IssuerSigningKey = new RsaSecurityKey(rsa),
        //CryptoProviderFactory = new CryptoProviderFactory
        //{
        //    // отключаем кеширование ключа. Объект RSA — Disposable,
        //    // и если его закешировать, возможны ObjectDisposedException
        //    CacheSignatureProviders = false
        //}
    };
});

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("Default", policy => policy.RequireAuthenticatedUser());
    opts.AddPolicy("OnlyForClients", policy =>
    {
        policy.RequireClaim("typeUser", "online_service_app_auth.db_layer.Client");
    });
    opts.AddPolicy("OnlyForMasters", policy =>
    {
        policy.RequireClaim("typeUser", "online_service_app_auth.db_layer.Master");
    });
    opts.AddPolicy("OnlyForOrganization", policy =>
    {
        policy.RequireClaim("typeUser", "online_service_app_auth.db_layer.Orgaanization");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
