namespace Transport.Application.Features.SearchByKeyword;

public sealed record SearchTransportByKeywordResponse
{
    public Guid Id { get; init; }
        
    public DateTime CreatedDate { get; init; }

    public DateTime? UpdatedDate { get; init; }
        
    public string NumberPlate { get; init; }
        
    public byte MaxPassengersCount { get; init; }
        
    public TransportTypeResponse Type { get; init; }
}

public sealed record TransportTypeResponse
{
    public int Id { get; init; }
    
    public string Name { get; init; }
}