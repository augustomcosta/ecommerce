using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Repositories;

namespace ecommerce_api.Data.RepositoriesImpl;

public class GenericRepository<T> : IGenericRepository<T> where  T : ModelBase
{
    public Task<T> GetByIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}