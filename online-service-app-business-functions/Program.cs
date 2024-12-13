using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using online_service_app_business_functions.db_layer;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online-service-app", Version = "v1" });

    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<OnlineServiceDbContext>();
builder.Services.AddAuthentication(options =>
{
    // устанавливаем дефолтную схему как JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var publicKey = @"-----BEGIN PUBLIC KEY-----
MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQBHpw5FPR+2hizdJ+Xu1es7
h1inFu7L9FmHoQF81XPYDJIIKnl4DPpZPVm6yNs+G5qNVDEquXKsqcguihPg45OO
5dONdra5uxTlXfbz8+u7nNiYvLYGmrEOEIJFyHRv8hIzxhDzDbkheFTGTSf7gxmw
aElDK394WA46QlvdRPGoo8Ohc4ssCFPbNXqhN9G6daAFex6eyoEkZEJrUsJckDMQ
i8gCItKJdyETcOCSVUF5nU2jB2JsHBcQ917G6ZFxl/DqGvpqzTkjFsWbcPJ7VXQ/
YTOhWNu9arXAWENz1j5IQyby0h9UFrZJ/tXs+t1jRdRPH2ocboJz1JinXbRuuPbV
AgMBAAE=
-----END PUBLIC KEY-----";

    using var rsa = RSA.Create();
    rsa.ImportFromPem(publicKey);

    // устанавливаем параметры токена
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // указываем ключ для проверки подписи
        IssuerSigningKey = new RsaSecurityKey(rsa),
        CryptoProviderFactory = new CryptoProviderFactory
        {
            // отключаем кеширование ключа. Объект RSA — Disposable,
            // и если его закешировать, возможны ObjectDisposedException
            CacheSignatureProviders = false
        }
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
