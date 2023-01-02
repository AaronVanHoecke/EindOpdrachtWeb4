using RestaurantBL.Interfaces;
using RestaurantBL.Managers;
using RestaurantDL.Repositories;
using RestaurantRESTbeheerder.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
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
ILogger logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseLogURLMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
