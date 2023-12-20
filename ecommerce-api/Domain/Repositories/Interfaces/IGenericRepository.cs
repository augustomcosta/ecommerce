using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Specifications.Interfaces;

namespace ecommerce_api.Domain.Repositories.Interfaces;

public interface IGenericRepository<T> where T : ModelBase
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> specification);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}