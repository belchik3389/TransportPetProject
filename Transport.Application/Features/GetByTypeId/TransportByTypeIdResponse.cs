namespace Transport.Application.Features.GetByTypeId;

//record - потому что DTO
public sealed record TransportByTypeIdResponse
{
    public Guid Id { get; set; }
        
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
        
    public required string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
        
    public required int TypeId { get; set; }
    
    public required string TypeName { get; init; }
}