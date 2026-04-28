using MediatR;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Transport.Domain.Contracts;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Application.Features.CreateTransport;

public sealed record CreateTransportRequest : IRequest<Guid>
{
    public string NumberPlate { get; set; }
        
    public byte MaxPassengersCount { get; set; }
    
    public int TypeId { get; set; }
}

public sealed class CreateTransportHandler : IRequestHandler<CreateTransportRequest, Guid>
{
    private readonly ILogger<CreateTransportHandler> _logger;
    private readonly ITransportRepository _transportRepository;
    
    public CreateTransportHandler(
        ITransportRepository transportRepository,
        ILogger<CreateTransportHandler> logger)
    {
        _logger = logger;
        _transportRepository = transportRepository;
    }
    
    public async Task<Guid> Handle(CreateTransportRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating transport with number plate {NumberPlate}, type id {TransportTypeId}",
            request.NumberPlate,
            request.TypeId);

        if (await _transportRepository.ExistsByNumberPlate(request.NumberPlate, cancellationToken))
        {
            _logger.LogWarning(
                "Transport with number plate {NumberPlate} already exists",
                request.NumberPlate);

            throw new ValidationException([
                new ValidationFailure(nameof(request.NumberPlate), $"Transport with number plate '{request.NumberPlate}' already exists.")
            ]);
        }
        
        var transport = new TransportEntity
        {
            TypeId = request.TypeId,
            NumberPlate = request.NumberPlate,
            MaxPassengersCount = request.MaxPassengersCount
        };
        
        var transportId = await _transportRepository.Create(transport, cancellationToken);

        _logger.LogInformation(
            "Transport with id {TransportId} and number plate {NumberPlate} was created",
            transportId,
            request.NumberPlate);

        return transportId;
    }
}
