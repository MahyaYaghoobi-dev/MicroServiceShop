using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository(ICatalogContext context) : IBrandRepository
{
    public async Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync()
    {
        return  await context.Brands.Find(_=>true).ToListAsync();
    }
}