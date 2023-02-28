using System;
using System.Text.Json.Serialization;
using Ecommerce.Db;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.DTOs;

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


// Add singleton services
builder.Services.AddSingleton<ICrudService<User, UserDTO>, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext is not null)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
