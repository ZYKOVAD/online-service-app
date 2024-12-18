using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using online_service_app_auth;
using online_service_app_auth.db_layer;
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
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<jwtProvider>();

var app = builder.Build();

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
