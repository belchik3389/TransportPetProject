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
        services.AddDbContext<TransportContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(nameof(TransportContext))));
        
        services.AddScoped<ITransportRepository, TransportRepository>();
        
        return services;
    }
}