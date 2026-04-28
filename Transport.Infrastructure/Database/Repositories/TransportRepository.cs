using Microsoft.EntityFrameworkCore;
using Transport.Domain.Contracts;
using Transport.Domain.Entities;
using Transport.Domain.Models;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Infrastructure.Database.Repositories;

internal sealed class TransportRepository : ITransportRepository
{
    private readonly DbSet<TransportEntity> _transportEntityDbSet;
    private readonly DbSet<TransportType> _typeEntityDbSet;
    private readonly DbContext _context;
    
    public TransportRepository(TransportContext context)
    {
        _transportEntityDbSet = context.Set<TransportEntity>();
        _typeEntityDbSet = context.Set<TransportType>();
        _context = context;
    }
    
    public async Task<TransportEntity?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _transportEntityDbSet
            .AsNoTracking() //Только чтение
            .Include(x => x.Type)
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
    
    public async Task<TransportType?> GetByTypeId(int id, CancellationToken cancellationToken)
    {
        return await _typeEntityDbSet
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<List<TransportEntity>> Search(SearchTransportCriteria searchCriteria, CancellationToken cancellationToken)
    {
        var query = _transportEntityDbSet.AsNoTracking();

        if (!string.IsNullOrEmpty(searchCriteria.Keyword))
        {
            var keyword = $"%{ searchCriteria.Keyword }%"; //поиск по частичному совпадению
            query = query.Where(x =>
                EF.Functions.ILike(x.Type.Name, keyword)
                || EF.Functions.ILike(x.NumberPlate, keyword));
        }

        if (searchCriteria.Take > 0)
        {
            query = query
                .Skip(searchCriteria.Skip) //пропускаем первые Skip записей
                .Take(searchCriteria.Take); //берем следующие Take записей
        }
        
        return await query
            .Include(x => x.Type)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> Create(TransportEntity transportCriteria, CancellationToken cancellationToken)
    {
        var entry = await _transportEntityDbSet.AddAsync(transportCriteria, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entry.Entity.Id;
    }
}
