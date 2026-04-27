using MediatR;
using Transport.Domain.Contracts;
using Transport.Domain.Models;

namespace Transport.Application.Features.SearchByKeyword;

public sealed record SearchTransportByKeywordRequest(string Keyword) : IRequest<List<SearchTransportByKeywordResponse>>;

internal sealed class SearchTransportByKeywordHandler
    : IRequestHandler<SearchTransportByKeywordRequest, List<SearchTransportByKeywordResponse>>
{
    private readonly ITransportRepository _transportRepository;
    
    public SearchTransportByKeywordHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }
    
    public async Task<List<SearchTransportByKeywordResponse>> Handle(SearchTransportByKeywordRequest request, CancellationToken cancellationToken)
    {
        var criteria = new SearchTransportCriteria
        {
            Keyword = request.Keyword,
        };
        
        var transports = await _transportRepository.Search(criteria, cancellationToken);

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