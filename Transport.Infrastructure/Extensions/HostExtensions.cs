using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transport.Infrastructure.Database;

namespace Transport.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task MigrateDatabaseSchema(this WebApplication application,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var scope = application.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TransportContext>();

            application.Logger.LogInformation("Applying database migrations");
            await dbContext.Database.MigrateAsync(cancellationToken);
            application.Logger.LogInformation("Database migrations applied");
        }
        catch (Exception exception)
        {
            application.Logger.LogError(exception, "Database migration failed during application startup");
            throw;
        }
    }
}
