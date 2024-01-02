using System.Collections;
using ecommerce_api.Data.Context;
using ecommerce_api.Data.RepositoriesImpl;
using ecommerce_api.Data.UnityOfWork.Interfaces;
using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Repositories.Interfaces;

namespace ecommerce_api.Data.UnityOfWork;

public class UnityOfWork : IUnityOfWork
{
    private readonly AppDbContext _context;
    private Hashtable _repositories;
    
    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ModelBase
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}