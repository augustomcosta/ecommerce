using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Specifications.Interfaces;

namespace ecommerce_api.Domain.Repositories;

public interface IGenericRepository<T> where T : ModelBase
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
}