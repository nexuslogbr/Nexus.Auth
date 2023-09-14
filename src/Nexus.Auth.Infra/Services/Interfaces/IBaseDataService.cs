using Nexus.Auth.Domain.Entities;
using System.Linq.Expressions;

namespace Nexus.Auth.Infra.Services.Interfaces;

public interface IBaseDataService<TEntity> : IBaseReadOnlyDataService<TEntity> where TEntity : EntityBase
{
    TEntity Add(TEntity entity);
    Task<TEntity> AddAsync(TEntity entity);
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

    TEntity Update(TEntity entity);
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

    TEntity DeleteOne(TEntity entity);
    Task<TEntity> DeleteByIdAsync(int id);
    IEnumerable<TEntity> DeleteMany(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter);

    TEntity DisableOne(TEntity entity);
    Task<TEntity> DisableByIdAsync(int id);
    IEnumerable<TEntity> DisableMany(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> DisableByFilterAsync(Expression<Func<TEntity, bool>> filter);

    Task<bool> SaveChangesAsync();
}