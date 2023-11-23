using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.Specifications;

public class SpecificationEvaluator<TEntity> where TEntity : ModelBase
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        query = spec.Includes.Aggregate(query, (current, include) =>
            current.Include(include));

        return query;
    }
}