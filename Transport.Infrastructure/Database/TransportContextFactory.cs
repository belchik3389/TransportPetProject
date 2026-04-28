using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Transport.Infrastructure.Database;

//Позволяет EF Tools создать DbContext напрямую, без необходимости поднимать startup-project
//Четко указываем как создать context
internal sealed class TransportContextFactory : IDesignTimeDbContextFactory<TransportContext>
{
    public TransportContext CreateDbContext(string[] args)
    {
        const string connectionString = "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=transport_db;";

        var optionsBuilder = new DbContextOptionsBuilder<TransportContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TransportContext(optionsBuilder.Options);
    }
}
