using Transport.Domain.Interfaces;

namespace Transport.Domain.Entities;

public class Transport : IAuditableDateTime
{
    public Guid Id { get; set; }
        
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
        
    public required string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
        
    public required int TypeId { get; set; }
        
    public TransportType Type { get; set; }
}