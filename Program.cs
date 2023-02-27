using System.Text.Json.Serialization;
using Ecommerce.Db;
using Ecommerce.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add JsonStringEnumCoverter option.
builder.Services.AddControllers()
    .AddJsonOptions(option => option.JsonSerializerOptions
    .Converters
    .Add(new JsonStringEnumConverter()));

// Add databases context
builder.Services.AddDbContext<AppDbContext>();

// Add configurations
builder.Services.Configure<PasswordSetting>(
    builder.Configuration.GetSection("PasswordSettings"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
