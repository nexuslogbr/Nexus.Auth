using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Services.Interfaces;
using System.Linq.Expressions;
using Nexus.Auth.Infra.Context;

namespace Nexus.Auth.Infra.Services;

public class BaseDataService<TEntity> : BaseReadOnlyDataService<TEntity>, IBaseDataService<TEntity> where TEntity : EntityBase
{
    public BaseDataService(NexusAuthContext context) : base(context)
    {
    }

    public TEntity Add(TEntity entity)
    {
        var entityEntry = _dbSet.Add(entity);
        return entityEntry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        return entityEntry.Entity;
    }

    public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
        return entities;
    }

    public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return entities;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities;
    }

    public TEntity DeleteOne(TEntity entity)
    {
        return _dbSet.Remove(entity).Entity;
    }

    public async Task<TEntity> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return DeleteOne(entity);
    }

    public IEnumerable<TEntity> DeleteMany(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return entities;
    }

    public async Task<IEnumerable<TEntity>> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = await GetAsync(filter: filter);
        return DeleteMany(entities);
    }

    public TEntity DisableOne(TEntity entity)
    {
        entity.Blocked = true;
        return Update(entity);
    }

    public async Task<TEntity> DisableByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return DisableOne(entity);
    }

    public IEnumerable<TEntity> DisableMany(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.Blocked = true;
        return UpdateRange(entities);
    }

    public async Task<IEnumerable<TEntity>> DisableByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = await GetAsync(filter: filter);
        foreach (var entity in entities)
            entity.Blocked = false;
        return UpdateRange(entities);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}