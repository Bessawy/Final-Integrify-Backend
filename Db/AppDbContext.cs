namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ecommerce.Models;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    public DbSet<User> Users {get; set;} = null!;
    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Category> Categories {get; set;} = null!;

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
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map enum to postgres
        modelBuilder.HasPostgresEnum<User.UserRole>();

        // Create index 
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // On delete set to Null
        //modelBuilder.Entity<Product>()
        //    .HasOne(p => p.Category)
        //    .WithOne()
         //   .OnDelete(DeleteBehavior.SetNull);

        // Always Automatic Load category with student (one to one relationship) Not valid!
        // modelBuilder.Entity<Product>()
        //    .Navigation(p => p.Category);

        // Set timestamp dafault values
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name));

        foreach(var p in properties)
            p.HasDefaultValueSql("CURRENT_TIMESTAMP");

        base.OnModelCreating(modelBuilder);
    }
}