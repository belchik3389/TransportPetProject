using Microsoft.EntityFrameworkCore;
using Transport.Domain.Contracts;
using Transport.Domain.Models;
using TransportEntity = Transport.Domain.Entities.Transport;

namespace Transport.Infrastructure.Database.Repositories;

internal sealed class TransportRepository : ITransportRepository
{
    private readonly DbSet<TransportEntity> _entitiesDbSet;
    
    public TransportRepository(TransportContext context)
    {
        _entitiesDbSet = context.Set<TransportEntity>();
    }
    
    public async Task<TransportEntity?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _entitiesDbSet
            .AsNoTracking() //Только чтение
            .Include(x => x.Type)
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    // TODO: IReadOnlyList
    public async Task<List<TransportEntity>> Search(SearchTransportCriteria searchCriteria, CancellationToken cancellationToken)
    {
        var query = _entitiesDbSet.AsNoTracking();

        // TODO: я бы стырил вот это var keyword = LikeExpressionHelper.ToSubstringPattern(searchCriteria.Keyword);			query = query.Where(x =>
        // 	EF.Functions.ILike(x.Title, keyword, LikeExpressionHelper.EscapeCharacter)
        if (!string.IsNullOrEmpty(searchCriteria.Keyword))
        {
            var keyword = $"%{ searchCriteria.Keyword }%"; //поиск по частичному совпадению
            query = query.Where(x =>
                EF.Functions.ILike(x.Type.Name, keyword)
                || EF.Functions.ILike(x.NumberPlate, keyword));
        }

        // TODO: сделал бы базовый репозиторий и Skip, Take вынес туда т.к. это везде будет практически
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
}