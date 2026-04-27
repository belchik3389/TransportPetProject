namespace Transport.Application.Features.GetById;

//record - потому что DTO
public sealed record TransportByIdResponse
{
    public Guid Id { get; set; }
        
    public string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
        
    public string TypeName { get; set; }
}