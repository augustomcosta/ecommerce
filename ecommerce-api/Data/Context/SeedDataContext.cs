using System.Text.Json;
using ecommerce_api.Domain.Entities;

namespace ecommerce_api.Data.Context;

public class SeedDataContext
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Brands.Any())
        {
            var brandsData = File.ReadAllText("../ecommerce-api/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.Brands.AddRange(brands);
        }
        
        if (!context.Categories.Any())
        {
            var categoriesData = File.ReadAllText("../ecommerce-api/Data/SeedData/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData); 
            context.Categories.AddRange(categories);
        }
        
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../ecommerce-api/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData); 
            context.Products.AddRange(products);
        }
        
        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        
    }
}