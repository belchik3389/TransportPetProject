using Transport.Domain.Entities;
using Transport.Domain.Models;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Domain.Contracts;

public interface ITransportRepository
{
    Task<TransportEntity?> GetById(
        Guid id,
        CancellationToken cancellationToken);

    Task<TransportType?> GetByTypeId(
        int id,
        CancellationToken cancellationToken);
    
    Task<List<TransportEntity>> Search(
        SearchTransportCriteria searchCriteria,
        CancellationToken cancellationToken);

    Task<Guid> Create(
        TransportEntity transportCriteria,
        CancellationToken cancellationToken);
}