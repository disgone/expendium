using Expendium.Data.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Expendium.Data.Interceptors;

public class AuditDateChangeInterceptor : SaveChangesInterceptor
{
    private static readonly Lazy<AuditDateChangeInterceptor> _instance =
       new(() => new AuditDateChangeInterceptor());

    private AuditDateChangeInterceptor()
    {
    }

    public static AuditDateChangeInterceptor Instance
    {
        get { return _instance.Value; }
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is null)
        {
            return result;
        }

        var entries = eventData.Context.ChangeTracker.Entries<IAudited>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
            }
        }
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}
