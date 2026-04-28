using MediatR;
using Microsoft.Extensions.Logging;
using Transport.Domain.Contracts;
using Transport.Domain.Models;

namespace Transport.Application.Features.SearchByKeyword;

public sealed record SearchTransportByKeywordRequest(string Keyword) : IRequest<List<SearchTransportByKeywordResponse>>;

internal sealed class SearchTransportByKeywordHandler
    : IRequestHandler<SearchTransportByKeywordRequest, List<SearchTransportByKeywordResponse>>
{
    private readonly ITransportRepository _transportRepository;
    private readonly ILogger<SearchTransportByKeywordHandler> _logger;
    
    public SearchTransportByKeywordHandler(
        ITransportRepository transportRepository,
        ILogger<SearchTransportByKeywordHandler> logger)
    {
        _transportRepository = transportRepository;
        _logger = logger;
    }
    
    public async Task<List<SearchTransportByKeywordResponse>> Handle(SearchTransportByKeywordRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching transport by keyword {Keyword}", request.Keyword);

        var criteria = new SearchTransportCriteria
        {
            Keyword = request.Keyword,
        };
        
        var transports = await _transportRepository.Search(criteria, cancellationToken);

        _logger.LogInformation(
            "Transport search completed for keyword {Keyword}. Found {Count} items",
            request.Keyword,
            transports.Count);

        return transports.Select(x => new SearchTransportByKeywordResponse
        {
            Id = x.Id,
            CreatedDate = x.CreatedDate,
            UpdatedDate = x.UpdatedDate,
            NumberPlate = x.NumberPlate,
            MaxPassengersCount =  x.MaxPassengersCount,
            Type = new TransportTypeResponse
            {
                Id = x.Type.Id,
                Name = x.Type.Name,
            }
        }).ToList();
    }
}
