namespace Ecommerce.Db;

using Ecommerce.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    // Update the timestamp only when an entity is modified!
    private void UpdateTimeStamp(DbContextEventData eventData)
    {
        // Get all entries from changeTracker which has entity with a BaseModel
        // and state that has been modified.
        var entries = eventData.Context!.ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && (e.State == EntityState.Modified));
        
        // Update the UpdateAt timestamp for all the returned entries.
        foreach(var entry in entries)
            ((BaseModel)entry.Entity).UpdateAt = DateTime.Now;
        
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        UpdateTimeStamp(eventData);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateTimeStamp(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}