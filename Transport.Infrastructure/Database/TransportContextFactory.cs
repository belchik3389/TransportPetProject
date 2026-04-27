using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Transport.Infrastructure.Database;

//Позволяет EF Tools создать DbContext напрямую, без необходимости поднимать startup-project
//Четко указываем как создать context
internal sealed class TransportContextFactory : IDesignTimeDbContextFactory<TransportContext>
{
    public TransportContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var apiProjectPath = GetApiProjectPath();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(nameof(TransportContext))
            ?? throw new InvalidOperationException(
                $"Connection string '{nameof(TransportContext)}' was not found in '{apiProjectPath}'.");

        var optionsBuilder = new DbContextOptionsBuilder<TransportContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TransportContext(optionsBuilder.Options);
    }

    private static string GetApiProjectPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory is not null)
        {
            var apiProjectPath = Path.Combine(currentDirectory.FullName, "Transport.Api");

            if (Directory.Exists(apiProjectPath))
            {
                return apiProjectPath;
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new DirectoryNotFoundException("Could not find the 'Transport.Api' project directory.");
    }
}
