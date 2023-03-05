namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ecommerce.Models;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    DbSet<User> Users {get; set;} = null!;

    public AppDbContext(IConfiguration config) => _config = config;

    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<User.UserRole>();

        // Use legacy timestamp behaviour
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var db_connection = _config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(db_connection)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map enum to postgres
        modelBuilder.HasPostgresEnum<User.UserRole>();

        // Create index 
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email);

        base.OnModelCreating(modelBuilder);
    }


}