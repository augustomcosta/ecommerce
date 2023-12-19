﻿using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    
}