using Transport.Domain.Models;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Domain.Contracts;

public interface ITransportRepository
{
    Task<TransportEntity?> GetById(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> ExistsByNumberPlate(
        string numberPlate,
        CancellationToken cancellationToken);

    
    Task<IReadOnlyList<TransportEntity>> Search(
        SearchTransportCriteria searchCriteria,
        CancellationToken cancellationToken);

    Task<Guid> Create(
        TransportEntity transportCriteria,
        CancellationToken cancellationToken);
}
