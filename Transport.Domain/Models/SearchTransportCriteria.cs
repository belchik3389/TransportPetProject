namespace Transport.Domain.Models;

public sealed record SearchTransportCriteria
{
    //TODO перенести в base репозиторий
    public int Skip { get; init; }
    
    public int Take { get; init; }
    
    public string Keyword { get; init; }
}