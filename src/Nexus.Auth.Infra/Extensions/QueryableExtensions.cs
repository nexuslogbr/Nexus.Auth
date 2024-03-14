using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using System.Linq.Expressions;

namespace Nexus.Auth.Infra.Extensions
{
    public interface IQueryFilter<TEntity> where TEntity : class
    {
        //public bool? Blocked { get; }
        IQueryable<TEntity> SetQuery(IQueryable<TEntity> queryable);
    }

    public interface IEntityQueryFilter<TEntity> : IQueryFilter<TEntity> where TEntity : EntityBase
    {
        public bool? Blocked { get; }
    }

    public interface IFilterPaginationParams<TEntity> : IQueryFilter<TEntity> where TEntity : class
    {
        int PageNumber { get; }
        public int PageSize { get; }
        //public string Term { get; }
        string OrderByProperty { get; }
        bool Asc { get; }
    }

    public interface IEntityFilterPaginationParams<TEntity> : IFilterPaginationParams<TEntity>, IEntityQueryFilter<TEntity> where TEntity : EntityBase
    {
    }

    public abstract class PaginationParams<TEntity> : IFilterPaginationParams<TEntity> where TEntity : class
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public abstract IQueryable<TEntity> SetQuery(IQueryable<TEntity> queryable);
        public string OrderByProperty { get; set; } = "";
        public bool Asc { get; set; } = true;
    }

    public abstract class EntityPaginationParams<TEntity> : PaginationParams<TEntity>, IEntityFilterPaginationParams<TEntity> where TEntity : EntityBase
    {
        public bool? Blocked { get; set; }
    }


    public abstract class EntityQueryFilter<TEntity> : IEntityQueryFilter<TEntity> where TEntity : EntityBase
    {
        public bool? Blocked { get; set; }
        public abstract IQueryable<TEntity> SetQuery(IQueryable<TEntity> queryable);
    }

    public class PaginationResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }

        //public PaginationResult() { }

        public PaginationResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = items;
        }
    }




    public static class QueryableExtensions
    {

        public static Task<PaginationResult<TSelect>> PaginateSelectAsync<TEntity, TSelect>(
            this IQueryable<TEntity> source,
            IFilterPaginationParams<TSelect> pageParams,
            Expression<Func<TEntity, TSelect>> select)
            where TEntity : class
            where TSelect : class
        {
            var query = source.Select(select).Filter(pageParams);
            return PaginateAsync(query, pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc);
        }

        //public static Task<PaginationResult<TSelect>> PaginateSelectAsync<TEntity, TSelect>(
        //    this IQueryable<TEntity> source,
        //    IFilterPaginationParams<TEntity> pageParams,
        //    Expression<Func<TEntity, TSelect>> select) where TEntity : class
        //{
        //    return PaginateAsync(source.FilterAsync(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc, query => query.Select(select));
        //}

        public static Task<PaginationResult<TSelect>> PaginateSelectAsync<TEntity, TSelect>(
             this IQueryable<TEntity> source,
             IEntityFilterPaginationParams<TEntity> pageParams,
             Expression<Func<TEntity, TSelect>> select) where TEntity : EntityBase
        {
            return PaginateAsync(source.FilterEntity(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc, query => query.Select(select));
        }



        public static Task<PaginationResult<TSelect>> PaginateAsync<TEntity, TSelect>(
            this IQueryable<TEntity> source,
            IFilterPaginationParams<TEntity> pageParams,
            Func<IQueryable<TEntity>, IQueryable<TSelect>> afterFilter) where TEntity : class
        {

            return PaginateAsync(source.Filter(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc, afterFilter);
        }

        public static Task<PaginationResult<TSelect>> PaginateAsync<TEntity, TSelect>(
           this IQueryable<TEntity> source,
           IEntityFilterPaginationParams<TEntity> pageParams,
           Func<IQueryable<TEntity>, IQueryable<TSelect>> afterFilter) where TEntity : EntityBase
        {

            return PaginateAsync(source.FilterEntity(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc, afterFilter);
        }

        public static Task<PaginationResult<TEntity>> PaginateAsync<TEntity>(
            this IQueryable<TEntity> source,
            IFilterPaginationParams<TEntity> pageParams) where TEntity : class
        {

            return PaginateAsync(source.Filter(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc);
        }

        public static Task<PaginationResult<TEntity>> PaginateAsync<TEntity>(
           this IQueryable<TEntity> source,
           IEntityFilterPaginationParams<TEntity> pageParams) where TEntity : EntityBase
        {

            return PaginateAsync(source.FilterEntity(pageParams), pageParams.PageNumber, pageParams.PageSize, pageParams.OrderByProperty, pageParams.Asc);
        }

        public static async Task<PaginationResult<TEntity>> PaginateAsync<TEntity>(
            this IQueryable<TEntity> source,
            int pageNumber,
            int pageSize,
            string? orderBy,
            bool asc) where TEntity : class
        {
            var query = source.AsNoTracking();


            if (!string.IsNullOrEmpty(orderBy))
                query = query.OrderBy(orderBy, !asc);

            //if (filter is not null)
            //    query = filter(query);

            var total = await query.CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
                query = query.Take(pageSize);
            }

            var result = await query.ToListAsync();
            return new PaginationResult<TEntity>(result, total, pageNumber, pageSize);
        }


        public static async Task<PaginationResult<TSelect>> PaginateAsync<TEntity, TSelect>(
            this IQueryable<TEntity> source,
            int pageNumber,
            int pageSize,
            string? orderBy,
            bool asc,
            Func<IQueryable<TEntity>, IQueryable<TSelect>> afterFilter) where TEntity : class
        {

            ArgumentNullException.ThrowIfNull(afterFilter, nameof(afterFilter));
            var query = afterFilter(source.AsNoTracking());


            if (!string.IsNullOrEmpty(orderBy))
                query = query.OrderBy(orderBy, !asc);


            var total = await query.CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
                query = query.Take(pageSize);
            }


            var result = await query.ToListAsync();
            return new PaginationResult<TSelect>(result, total, pageNumber, pageSize);
        }




        public static IQueryable<TEntity> Filter<TEntity>(
          this IQueryable<TEntity> source,
          IQueryFilter<TEntity>? filter = null) where TEntity : class
        {

            if (filter != null)
                source = filter.SetQuery(source);

            return source;
        }

        public static IQueryable<TEntity> FilterEntity<TEntity>(
          this IQueryable<TEntity> source,
          IEntityQueryFilter<TEntity>? filter = null) where TEntity : EntityBase
        {

            source = source.Filter(filter);


            if (filter?.Blocked != null)
                source = source.Where(x => x.Blocked == filter.Blocked.Value);

            return source;
        }
    }
}
