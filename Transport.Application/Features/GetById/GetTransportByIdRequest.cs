using MediatR;
using Microsoft.Extensions.Logging;
using Transport.Domain.Contracts;

namespace Transport.Application.Features.GetById;

public sealed record GetTransportByIdRequest(Guid Id) : IRequest<TransportByIdResponse?>;

internal sealed class GetTransportByIdHandler : IRequestHandler<GetTransportByIdRequest, TransportByIdResponse?>
{
    private readonly ITransportRepository _transportRepository;
    private readonly ILogger<GetTransportByIdHandler> _logger;
    
    public GetTransportByIdHandler(
        ITransportRepository transportRepository,
        ILogger<GetTransportByIdHandler> logger)
    {
        _transportRepository = transportRepository;
        _logger = logger;
    }
    
    public async Task<TransportByIdResponse?> Handle(GetTransportByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting transport by id {TransportId}", request.Id);

        var transport = await _transportRepository.GetById(request.Id, cancellationToken);

        if (transport is null)
        {
            _logger.LogWarning("Transport with id {TransportId} was not found", request.Id);
            return null;
        }

        _logger.LogInformation("Transport with id {TransportId} was found", request.Id);
        
        // if (transport.Type is null)
        // {
        //     throw new InvalidOperationException($"Transport {transport.Id} loaded without Type. Missing Include?");
        // }
        
        return new()
        {
            Id = transport.Id,
            NumberPlate = transport.NumberPlate,
            MaxPassengersCount = transport.MaxPassengersCount,
            TypeName = transport.Type.Name, //NavigationProperty
            TypeId = transport.Type.Id,
        };
    }
}
