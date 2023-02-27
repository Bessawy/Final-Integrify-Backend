namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    DbSet<User> Users {get; set;}

    public AppDbContext(IConfiguration config) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var db_connection = _config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(db_connection)
            .UseSnakeCaseNamingConvention();
    }


}