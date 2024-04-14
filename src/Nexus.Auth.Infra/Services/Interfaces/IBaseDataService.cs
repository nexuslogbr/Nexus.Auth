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

    TEntity ChangeStatus(TEntity entity, bool status);
    Task<TEntity> ChangeStatusByIdAsync(int id, bool status);
    IEnumerable<TEntity> ChangeStatusRange(IEnumerable<TEntity> entities, bool status);
    Task<IEnumerable<TEntity>> ChangeStatusByFilterAsync(Expression<Func<TEntity, bool>> filter, bool status);

    Task<bool> SaveChangesAsync();
}