using System.Linq.Expressions;

namespace ecommerce_api.Domain.Specifications.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    
    List<Expression<Func<T, object>>> Includes { get; }
}