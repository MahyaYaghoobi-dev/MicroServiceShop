using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(ProductSpecParams specParams, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Empty;

       
        var query = FilterProducts(specParams, filter);

        
        query = SortProducts(specParams, query);

        
        var result = await PaginationProduct(specParams, cancellationToken, query);

        return result;
    }

    private static async Task<List<Product>> PaginationProduct(ProductSpecParams specParams, CancellationToken cancellationToken,
        IFindFluent<Product, Product> query)
    {
        var skip = (specParams.PageIndex - 1) * specParams.PageSize;
        var result = await query.Skip(skip).Limit(specParams.PageSize).ToListAsync(cancellationToken);
        return result;
    }

    private static IFindFluent<Product, Product> SortProducts(ProductSpecParams specParams, IFindFluent<Product, Product> query)
    {
        
            if (string.IsNullOrEmpty(specParams.SortBy))
                return query;

            var isDesc = specParams.SortOrder == SortOrder.Desc;

            return specParams.SortBy.ToLower() switch
            {
                "name" => isDesc ? query.SortByDescending(p => p.Name) : query.SortBy(p => p.Name),
                "price" => isDesc ? query.SortByDescending(p => p.Price) : query.SortBy(p => p.Price),
                _ => query.SortBy(p => p.Name)
            };
        
    }

    private IFindFluent<Product, Product> FilterProducts(ProductSpecParams specParams, FilterDefinition<Product> filter)
    {
        if (!string.IsNullOrEmpty(specParams.BrandId))
            filter &= Builders<Product>.Filter.Eq(p => p.Brand.Id, specParams.BrandId);

        if (!string.IsNullOrEmpty(specParams.TypeId))
            filter &= Builders<Product>.Filter.Eq(p => p.Type.Id, specParams.TypeId);

        if (!string.IsNullOrEmpty(specParams.Search))
            filter &= Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(specParams.Search, "i"));

        var query = context.Products.Find(filter);
        return query;
    }

    public async Task<long> GetProductsCountAsync(ProductSpecParams specParams, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Product>.Filter.Empty;

        if (!string.IsNullOrEmpty(specParams.BrandId))
            filter &= Builders<Product>.Filter.Eq(p => p.Brand.Id, specParams.BrandId);

        if (!string.IsNullOrEmpty(specParams.TypeId))
            filter &= Builders<Product>.Filter.Eq(p => p.Type.Id, specParams.TypeId);

        if (!string.IsNullOrEmpty(specParams.Search))
            filter &= Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(specParams.Search, "i"));

        return await context.Products.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
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