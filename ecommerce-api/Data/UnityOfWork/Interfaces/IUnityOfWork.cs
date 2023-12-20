using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Repositories.Interfaces;

namespace ecommerce_api.Data.UnityOfWork.Interfaces;

public interface IUnityOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ModelBase;

    Task<int> Complete();
}