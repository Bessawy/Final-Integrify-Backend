namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;

static class PorpertyConfigExtension
{
    // Add default values for all DateTime properties.
    public static void AddDateTimeDefualtToAll(this ModelBuilder modelBuilder)
    {
        // Get all DateTime properties.
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name));

        // Set all DateTime properties default values.
        foreach(var p in properties)
            p.HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}