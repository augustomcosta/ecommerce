using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Repositories;

public interface IGenericRepository<T> where T : ModelBase
{
    Task<T> GetByIdAsync();
    Task<IEnumerable<T>> GetAllAsync();
}