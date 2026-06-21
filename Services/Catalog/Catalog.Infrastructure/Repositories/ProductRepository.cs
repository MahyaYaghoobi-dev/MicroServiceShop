using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await context.Products.Find(x => true).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(string productId)
    {
        return await context.Products.Find(x => x.Id == productId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
    {
        return await context.Products.Find(x=>x.Name.Contains(productName,StringComparison.OrdinalIgnoreCase)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId)
    {
        return await context.Products.Find(x=>x.Brand.Id==brandId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId)
    {
        return await context.Products.Find(x=>x.Type.Id==typeId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName)
    {
        return await  context.Products.Find(x => x.Brand.Name.Contains(brandName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string typeName)
    {
       return await context.Products.Find(x => x.Type.Name.Contains(typeName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
       var result= await context.Products.ReplaceOneAsync(x=>x.Id == product.Id, product);
       return result.IsAcknowledged;
    }

    public async Task<bool> DeleteProductAsync(string productId)
    {
        var result=await context.Products.DeleteOneAsync(x=>x.Id==productId);
        return result.IsAcknowledged;
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        try
        {
            await context.Products.InsertOneAsync(product);
            return product;
        }
        catch
        {
            return null;
        }
    }
}