using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Transport.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationReferences(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        return services;
    }
}
