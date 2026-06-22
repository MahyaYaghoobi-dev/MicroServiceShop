using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await context.Products.Find(x => true).ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(string productId,CancellationToken cancellationToken)
    {
        return await context.Products.Find(x => x.Id == productId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName,CancellationToken cancellationToken)
    {
        return await context.Products.Find(x=>x.Name.Contains(productName,StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId,CancellationToken cancellationToken)
    {
        return await context.Products.Find(x=>x.Brand.Id==brandId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId,CancellationToken  cancellationToken)
    {
        return await context.Products.Find(x=>x.Type.Id==typeId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName,CancellationToken cancellationToken)
    {
        return await  context.Products.Find(x => x.Brand.Name.Contains(brandName, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string typeName,CancellationToken cancellationToken)
    {
       return await context.Products.Find(x => x.Type.Name.Contains(typeName, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateProductAsync(Product product,CancellationToken cancellationToken)
    {
       var result= await context.Products.ReplaceOneAsync(x=>x.Id == product.Id, product,cancellationToken:cancellationToken);
       return result.IsAcknowledged;
    }

    public async Task<bool> DeleteProductAsync(string productId,CancellationToken cancellationToken)
    {
        var result=await context.Products.DeleteOneAsync(x=>x.Id==productId,cancellationToken:cancellationToken);
        return result.IsAcknowledged;
    }

    public async Task<Product?> CreateProductAsync(Product product,CancellationToken cancellationToken)
    {
        try
        {
            await context.Products.InsertOneAsync(product,cancellationToken:cancellationToken);
            return product;
        }
        catch
        {
            return null;
        }
    }
}