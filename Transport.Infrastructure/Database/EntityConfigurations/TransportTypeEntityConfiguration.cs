using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Domain.Entities;

namespace Transport.Infrastructure.Database.EntityConfigurations;

public class TransportTypeEntityConfiguration : IEntityTypeConfiguration<TransportType>
{
    public void Configure(EntityTypeBuilder<TransportType> builder)
    {
        builder
            .HasKey(x => x.Id);
            
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasData(GetDefaultTypes());
    }

    private static IList<TransportType> GetDefaultTypes()
    {
        return new List<TransportType>
        {
            new(1, "Самолет"),
            new(2, "Вертолет"),
            new(3, "Автобус"),
            new(4, "Микроавтобус"),
            new(5, "Легковой автомобиль"),
            new(6, "Минивэн"),
            new(7, "Внедорожник"),
            new(8, "Грузовик"),
            new(9, "Поезд"),
            new(10, "Катер")
        };
    }
}
