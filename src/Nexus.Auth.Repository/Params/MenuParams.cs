using Nexus.Auth.Repository.Dtos.Generics;
using System.Linq.Expressions;
using LinqKit;
using Nexus.Auth.Domain.Entities;


namespace Nexus.Auth.Repository.Params
{
    public class MenuParams : PageFilterParams<Menu>
    {
        public override Expression<Func<Menu, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Menu>();
            predicate = predicate.And(x => x.Name.Contains(Term));

            if (Blocked.HasValue)
                predicate = predicate.And(x => x.Blocked == Blocked);

            return predicate;
        }
    }
}
