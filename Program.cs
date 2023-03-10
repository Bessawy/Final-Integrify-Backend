using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.Db;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;

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

// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();

// Add scoped, transient or singleton services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductSurvice, DbProductService>();
builder.Services.AddScoped<ICategorySurvice, DbCategoryService>();

// Add authentication for two JWT tokens (users, customers)
builder.Services.AddAuthentication()
.AddJwtBearer("customers", options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt_customer:Issuer"],
        ValidAudience = builder.Configuration["Jwt_customer:Aud"],
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt_customer:Secret"])), 
    };
})
.AddJwtBearer("admins", options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt_admin:Issuer"],
        ValidAudience = builder.Configuration["Jwt_admin:Aud"],
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt_admin:Secret"])), 
    };
});

//Add authorization Policies
builder.Services.AddAuthorization(options =>
{
    // validiate one of both as a default using customers then admins tokens
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("customers", "admins")
        .Build();

    // validate admins
    options.AddPolicy("admin", new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("admins")
            .RequireRole("admin")
            .Build());
});


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
