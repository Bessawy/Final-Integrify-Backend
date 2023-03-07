namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

static class PorpertyConfigExtension
{
    public static void AddDateTimeDefualtToAll(this ModelBuilder modelBuilder)
    {
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name));

        foreach(var p in properties)
            p.HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}