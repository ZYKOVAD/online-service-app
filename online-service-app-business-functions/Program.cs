using online_service_app_business_functions.db_layer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OnlineServiceDbContext>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(opts =>
{

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
