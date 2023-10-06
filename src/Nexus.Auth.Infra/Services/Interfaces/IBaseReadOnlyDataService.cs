using Nexus.Auth.Domain.Entities;
using System.Linq.Expressions;

namespace Nexus.Auth.Infra.Services.Interfaces;

public interface IBaseReadOnlyDataService<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> AsQueryable();

    Task<IEnumerable<TEntity>> GetAsync(
        int? pageNumber = null,
        int? pageSize = null,
        Expression<Func<TEntity, bool>> filter = null,
        string orderBy = null,
        string includeProps = null,
        bool asNoTracking = true);

    Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> filter = null,
        string includeProps = null,
        bool asNoTracking = true);

    Task<IEnumerable<TEntity>> GetAllAsync(
        string? includeProps = null,
        bool asNoTracking = true);

    Task<TEntity> GetByIdAsync(int id, string includeProps = null);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> filter = null);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter = null);
}