using Nexus.Auth.Domain.Entities;
using System.Linq.Expressions;

namespace Nexus.Auth.Repository.Dtos.Generics
{
    public abstract class PageFilterParams<TEntity> where TEntity : EntityBase
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Term { get; set; } = string.Empty;
        public bool? Blocked { get; set; }
        public abstract Expression<Func<TEntity, bool>> Filter();

        public string OrderByProperty { get; set; } = "";
        public bool Asc { get; set; } = true;
    }
}
