using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using online_service_app_auth.db_layer;
using online_service_app_auth.repositories;
using online_service_app_auth.Repositories;
using online_service_app_auth.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddAuthorization();

builder.Services.AddCors();
builder.Services.AddScoped<OnlineServiceDbContext>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<MasterRepository>();
builder.Services.AddScoped<MasterService>();
builder.Services.AddScoped<OrganizationRepository>();
builder.Services.AddScoped<OrganizationService>();

var app = builder.Build();

builder.Configuration.AddJsonFile("SecretConfig.json");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseHttpsRedirection();  

app.MapControllers();

app.Run();
