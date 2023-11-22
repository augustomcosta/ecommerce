﻿using ecommerce_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base (options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    
}