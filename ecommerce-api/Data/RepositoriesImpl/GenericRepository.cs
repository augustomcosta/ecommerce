﻿using ecommerce_api.Data.Context;
using ecommerce_api.Data.Specifications;
using ecommerce_api.Domain.Entities.Base;
using ecommerce_api.Domain.Repositories;
using ecommerce_api.Domain.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.RepositoriesImpl;

public class GenericRepository<T> : IGenericRepository<T> where  T : ModelBase
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T>? spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}