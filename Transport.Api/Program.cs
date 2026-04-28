using Serilog;
using Transport.Application.Extensions;
using Transport.Api.Infrastructure;
using Transport.Infrastructure.Extensions;

namespace Transport.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureReferences(builder.Configuration);
            builder.Services.AddApplicationReferences(builder.Configuration);

            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });

            var app = builder.Build();

            app.UseExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI();
            //Публикация маршрутов контроллеров - доступны по HTTP
            app.MapControllers();
            
            //миграции при старте
            await app.MigrateDatabaseSchema();

            await app.RunAsync();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Application terminated unexpectedly");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
