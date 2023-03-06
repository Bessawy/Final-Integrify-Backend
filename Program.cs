using System;
using System.Text.Json.Serialization;
using Ecommerce.Db;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add JsonStringEnumCoverter option.
// Ignore Cycle created from relationships.
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());   
        options.JsonSerializerOptions.ReferenceHandler 
            = ReferenceHandler.IgnoreCycles;
    });

// Add databases context
builder.Services.AddDbContext<AppDbContext>();

// Add configurations


// Add singleton services
builder.Services.AddScoped<ICrudService<User, UserDTO>, DbUserService>();
builder.Services.AddScoped<ICrudService<Product, ProductDTO>, DbProductService>();

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
        var config = scope.ServiceProvider.GetService<IConfiguration>();

        if (dbContext is not null
                && config.GetValue<bool>("CreateDbAtStart", false))
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
