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


// Add services to the container.
builder.Services.AddControllers();
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