using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transport.Domain.Contracts;
using Transport.Infrastructure.Database;
using Transport.Infrastructure.Database.Repositories;

namespace Transport.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureReferences(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(TransportContext))
                               ?? throw new InvalidOperationException($"Строка подключения '{nameof(TransportContext)}' не найдена в настройках.");
        
        services.AddDbContext<TransportContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<ITransportRepository, TransportRepository>();
        
        return services;
    }
}