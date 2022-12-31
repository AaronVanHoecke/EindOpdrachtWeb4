using RestaurantBL.Interfaces;
using RestaurantBL.Managers;
using RestaurantDL.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connectionString = @"Data Source=WINDOWS-ISGC24U\SQLEXPRESS;Initial Catalog=RestaurantOpdracht;Integrated Security=True;TrustServerCertificate=True";
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IRestaurantRepository>(r => new RestaurantRepository(connectionString));
builder.Services.AddSingleton<RestaurantManager>();
builder.Services.AddSingleton<IReservatieRepository>(r => new ReservatieRepository(connectionString));
builder.Services.AddSingleton<ReservatieManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
