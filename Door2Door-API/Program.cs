using System.Data;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using Npgsql;

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
var configuration = provider.GetRequiredService<IConfiguration>(); // For testing. Delete the hardcoded string and add env variable.


// Add services to the container.
builder.Services.AddControllers();

// Injecting the IDbConnection for NpgSql
builder.Services.AddTransient<IDbConnection>((sp =>
    new NpgsqlConnection(configuration.GetConnectionString("NpgSqlConnection"))));

// Register repositories here
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

// DP injection for factory
builder.Services.AddScoped<IFactory<Room>, RoomFactory>();
builder.Services.AddScoped<IFactory<RoomType>, RoomTypeFactory>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

Console.WriteLine("Does this hit the run window???");



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