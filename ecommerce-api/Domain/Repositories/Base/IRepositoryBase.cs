namespace ecommerce_api.Domain.Repositories.Base;

public interface IRepositoryBase<T>
{
    Task<T> Create(T type);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int? id);
    Task<T> Update(T type, int? id);
    Task<T> Delete(int? id);
}