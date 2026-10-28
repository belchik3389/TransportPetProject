using MediatR;
using Microsoft.Extensions.Logging;
using Transport.Domain.Contracts;

namespace Transport.Application.Features.GetByTypeId;

public sealed record GetTransportByTypeIdRequest(int Id) : IRequest<TransportByTypeIdResponse[]>;

internal sealed class GetTransportByTypeIdHandler : IRequestHandler<GetTransportByTypeIdRequest, TransportByTypeIdResponse[]>
{
    private readonly ITransportRepository _transportRepository;
    private readonly ILogger<GetTransportByTypeIdHandler> _logger;
    
    public GetTransportByTypeIdHandler(
        ITransportRepository transportRepository,
        ILogger<GetTransportByTypeIdHandler> logger)
    {
        _transportRepository = transportRepository;
        _logger = logger;
    }
    
    public async Task<TransportByTypeIdResponse[]> Handle(GetTransportByTypeIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting transport by id {TransportId}", request.Id);

        var transport = await _transportRepository.GetByTypeId(request.Id, cancellationToken);

        _logger.LogInformation("Transport with id {TransportId} was found", request.Id);
        
        /*if (transport is null)
        {
            throw new InvalidOperationException($"Transports loaded without Type. Missing Include?");
        }*/

        return transport.Select(x => new TransportByTypeIdResponse
        {
            Id = x.Id,
            NumberPlate = x.NumberPlate,
            MaxPassengersCount = x.MaxPassengersCount,
            TypeName = x.Type.Name,
            TypeId = x.Type.Id,
        }).ToArray();

        /*return new()
        {
            Id = transport.Id,
            NumberPlate = transport.NumberPlate,
            MaxPassengersCount = transport.MaxPassengersCount,
            TypeName = transport.Type.Name, //NavigationProperty
            TypeId = transport.Type.Id,
        };*/
    }
}
