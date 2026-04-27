using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Transport.Domain.Interfaces;

namespace Transport.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void SetAuditableEntitiesCreateUpdateDates(this DbContext context)
    {
        var utcNow = DateTime.UtcNow;

        foreach (EntityEntry<IAuditableDateTime> entry in context.ChangeTracker.Entries<IAuditableDateTime>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = utcNow;
                    break;
                case EntityState.Added:
                    if (entry.Entity.CreatedDate == default)
                    {
                        entry.Entity.CreatedDate = utcNow;
                    }
                    
                    break;
            }
        }
    }
}
