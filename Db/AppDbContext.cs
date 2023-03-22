namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private readonly IConfiguration _config;

    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Category> Categories {get; set;} = null!;
    public DbSet<CartItem> Carts {get; set;} = null!;
    public DbSet<Review> Reviews {get; set;} = null!;

    public AppDbContext(IConfiguration config) => _config = config;

    static AppDbContext()
    {
        // Use legacy timestamp behaviour
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var db_connection = _config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(db_connection)
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Create index  
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Add primary key
        modelBuilder.Entity<CartItem>()
            .HasKey(c => new {c.UserId, c.ProductId});
        modelBuilder.Entity<Review>()
            .HasKey(c => new {c.UserId, c.ProductId});

        // On delete set to Null
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .OnDelete(DeleteBehavior.SetNull);

        // Set all datetime dafault values
        modelBuilder.AddDateTimeDefualtToAll();

        // Change AspCore Identity table names
        modelBuilder.AddIdentityConfig();
    }
}