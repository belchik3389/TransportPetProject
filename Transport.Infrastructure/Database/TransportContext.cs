using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Extensions;

namespace Transport.Infrastructure.Database;

internal sealed class TransportContext(DbContextOptions<TransportContext> options) : DbContext(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.SetAuditableEntitiesCreateUpdateDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        this.SetAuditableEntitiesCreateUpdateDates();

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Для наименования бд объектов в Snake Case стиле
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Берем из сборки все классы, которые реализуют IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransportContext).Assembly);
    }
}
