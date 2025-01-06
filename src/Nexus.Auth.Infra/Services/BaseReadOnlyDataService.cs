using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Services.Interfaces;
using System.Linq.Expressions;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Infra.Extensions;

namespace Nexus.Auth.Infra.Services;

public class BaseReadOnlyDataService<TEntity> : IBaseReadOnlyDataService<TEntity> where TEntity : EntityBase
{
    protected readonly NexusAuthContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseReadOnlyDataService(NexusAuthContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        int? pageNumber = null,
        int? pageSize = null,
        Expression<Func<TEntity, bool>> filter = null,
        string orderBy = null,
        bool asc = true,
        string includeProps = null,
        bool asNoTracking = true)
    {
        return await GetQueryable(pageNumber, pageSize, filter, orderBy, asc, includeProps, asNoTracking).ToListAsync();
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, string includeProps = null, bool asNoTracking = true)
    {
        return await GetQueryable(filter: filter, includeProps: includeProps, asNoTracking: asNoTracking).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(string? includeProps = null, bool asNoTracking = true)
    {
        return await GetQueryable(includeProps: includeProps, asNoTracking: asNoTracking).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id, string includeProps = null)
    {
        return await GetQueryable(includeProps: includeProps).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await GetQueryable(filter: filter).AnyAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await GetQueryable(filter: filter).CountAsync();
    }

    protected virtual IQueryable<TEntity> GetQueryable(
        int? pageNumber = null,
        int? pageSize = null,
        Expression<Func<TEntity, bool>> filter = null,
        string orderBy = null,
        bool asc = true,
        string includeProps = null,
        bool asNoTracking = true)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(includeProps))
            query = includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (q, p) => q.Include(p));

        if (filter is not null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(orderBy))
            query = query.OrderBy(orderBy, !asc);

        if (asNoTracking)
            query = query.AsNoTracking();

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
            query = query.Take(pageSize.Value);
        }

        return query;
    }
}