using MediatR;
using Transport.Domain.Contracts;

namespace Transport.Application.Features.GetById;

public sealed record GetTransportByIdRequest(Guid Id) : IRequest<TransportByIdResponse?>;

internal sealed class GetTransportByIdHandler : IRequestHandler<GetTransportByIdRequest, TransportByIdResponse?>
{
    private readonly ITransportRepository _transportRepository;
    
    public GetTransportByIdHandler(
        ITransportRepository  transportRepository)
    {
        _transportRepository = transportRepository;
    }
    
    public async Task<TransportByIdResponse?> Handle(GetTransportByIdRequest request, CancellationToken cancellationToken)
    {
        var transport = await _transportRepository.GetById(request.Id, cancellationToken);

        if (transport == null)
        {
            return null;
        }
        
        return new()
        {
            Id = transport.Id,
            NumberPlate = transport.NumberPlate,
            MaxPassengersCount = transport.MaxPassengersCount,
            TypeName = transport.Type.Name
        };
    }
}