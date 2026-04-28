namespace Transport.Application.Features.SearchByKeyword;

public sealed record SearchTransportByKeywordResponse
{
    public Guid Id { get; set; } // TODO: set -> init
        
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
        
    public string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
        
    public TransportTypeResponse Type { get; set; }
}

public sealed record TransportTypeResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}