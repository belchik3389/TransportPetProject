namespace Transport.Application.Features.GetById;

//record - потому что DTO
public sealed record TransportByIdResponse
{
    public required Guid Id { get; init; }
        
    public required string NumberPlate { get; init; }
        
    public required byte MaxPassengersCount { get; init; }
    
    public required int TypeId { get; init; }
        
    public required string TypeName { get; init; }
}