using TransportEntity = Transport.Domain.Entities.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Transport.Infrastructure.Database.EntityConfigurations;

public class TransportEntityConfiguration : IEntityTypeConfiguration<TransportEntity>
{
    public void Configure(EntityTypeBuilder<TransportEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
            
        builder
            .Property(x => x.NumberPlate)
            .IsRequired()
            .HasMaxLength(10);
            
        builder
            .Property(x => x.MaxPassengersCount)
            .IsRequired();
            
        builder
            .HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId);
            
        builder
            .HasIndex(x => x.NumberPlate)
            .IsUnique();
    }
}