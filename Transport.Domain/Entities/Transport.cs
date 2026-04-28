using Transport.Domain.Interfaces;

namespace Transport.Domain.Entities;

// TODO: я бы добаил Entity но в нестле вроде так можно оставить
public class Transport : IAuditableDateTime
{
    public Guid Id { get; set; }
        
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
        
    public required string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
        
    public int TypeId { get; set; }
        
    public required TransportType Type { get; set; }
}