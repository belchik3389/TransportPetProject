namespace Transport.Domain.Models;

public sealed record SearchTransportCriteria
{
    public int Skip { get; init; }
    
    public int Take { get; init; }
    
    public string Keyword { get; init; }
}