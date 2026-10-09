namespace Transport.Application.Features.SearchByKeyword;

public sealed record SearchTransportByKeywordResponse
{
    public required Guid Id { get; init; }

    public required DateTime CreatedDate { get; init; }

    public DateTime? UpdatedDate { get; init; }

    public required string NumberPlate { get; init; }

    public required byte MaxPassengersCount { get; init; }

    public required TransportTypeResponse Type { get; init; }
}

public sealed record TransportTypeResponse
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
