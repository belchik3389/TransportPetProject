namespace Transport.Domain.Models;

public sealed record SearchTransportCriteria
{
    // TODO Skip, Take я бы вынес в базовый класс, будет часто где
    public int Skip { get; init; }
    
    public int Take { get; init; }
    
    public string Keyword { get; init; }
}