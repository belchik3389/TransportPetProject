using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transport.Application.Behaviors;

namespace Transport.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationReferences(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);

        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
