namespace Transport.Application.Features.GetById;

//record - потому что DTO
public sealed record TransportByIdResponse
{
    public Guid Id { get; init; }
        
    public string NumberPlate { get; init; }
        
    public byte MaxPassengersCount { get; init; }
    
    public int TypeId { get; init; }
        
    public string TypeName { get; init; }
}