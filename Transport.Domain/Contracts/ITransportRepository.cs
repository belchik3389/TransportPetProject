using Transport.Domain.Models;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Domain.Contracts;

public interface ITransportRepository
{
    Task<TransportEntity?> GetById(
        Guid id,
        CancellationToken cancellationToken);

    // TODO IreadOnlyList
    Task<List<TransportEntity>> Search(
        SearchTransportCriteria searchCriteria,
        CancellationToken cancellationToken);
}