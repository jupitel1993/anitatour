using Sieve.Services;

namespace Application.Common.Sieve.CustomFilters
{
    // if many, make it partial
    public class AppFilterMethods : ISieveCustomFilterMethods
    {
        //private IQueryable<T> Search<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate, string op) where T : AuditableEntity
        //{
        //    var filterOp = op.GetFilterOperator();
        //    switch (filterOp)
        //    {
        //        case FilterOperator.Contains:
        //        {
        //            var result = source.Where(predicate);

        //            return result;
        //        }
        //        default: return Enumerable.Empty<T>().AsQueryable();
        //    }
        //}
    }
}