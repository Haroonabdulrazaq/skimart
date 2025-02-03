using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
private readonly StoreContext context = context;

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x => x.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
       return await context.Products.Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }

    void IProductRepository.AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    void IProductRepository.DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    async Task<Product?> IProductRepository.GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    async Task<IReadOnlyList<Product>> IProductRepository.GetProductsAsync()
    {
       return await context.Products.ToListAsync();
    }

    bool IProductRepository.ProductExists(int id)
    {
      return context.Products.Any(x => x.Id == id);
    }

    async Task<bool> IProductRepository.SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    void IProductRepository.UpdateProduct(Product product)
    {
       context.Entry(product).State = EntityState.Modified;
    }
}
