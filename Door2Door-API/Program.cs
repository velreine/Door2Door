using System.Data;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using Npgsql;
using RouteModel = Door2Door_API.Models.Route;


var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
var testConnectionString = Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING");

if (connectionString is null)
{
    throw new Exception(
        "EXCEPTION: ENVIRONMENT VARIABLE \"CONNECTION_STRING\" IS UNSET, THE API CANNOT RUN WITHOUT IT.");
}

if (testConnectionString is null)
{
    Console.WriteLine("WARNING: ENVIRONMENT VARIABLE \"TEST_CONNECTION_STRING\" IS UNSET, THIS MEANS TEST CANNOT RUN PROPERLY.");
}

if (connectionString is not null && testConnectionString is not null && testConnectionString == connectionString)
{
    throw new Exception(
        "EXCEPTION: ENVIRONMENT VARIABLE \"CONNECTION_STRING\" AND \"TEST_CONNECTION_STRING\" IS THE SAME, THIS IS SURELY NOT WHAT YOU WANT."
    );
}

var builder = WebApplication.CreateBuilder(args);

// Open CORS because YOLO
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyOrigin();
    }); 
});

// Access the IConfiguration service
var provider = builder.Services.BuildServiceProvider();

// Add services to the container.
builder.Services.AddControllers();

// Injecting the IDbConnection for NpgSql
builder.Services.AddTransient<IDbConnection>(sp =>
    new NpgsqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"))
);

// Register repositories here
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();

// DP injection for factory
builder.Services.AddScoped<IFactory<Room>, RoomFactory>();
builder.Services.AddScoped<IFactory<RoomType>, RoomTypeFactory>();
builder.Services.AddScoped<IFactory<RouteModel>, RouteFactory>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register PostGIS Type-mappings for Npgsql
NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OpenPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();