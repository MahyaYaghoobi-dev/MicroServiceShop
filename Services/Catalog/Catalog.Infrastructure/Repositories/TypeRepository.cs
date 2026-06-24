using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class TypeRepository(ICatalogContext context) : ITypeRepository
{
    public async Task<IEnumerable<ProductType>> GetAllProductTypesAsync(CancellationToken cancellationToken)
    {
        return await context.Types.Find(x => true).ToListAsync(cancellationToken:cancellationToken);
    }

    public async Task<ProductType?> GetProductTypeByIdAsync(string productTypeId, CancellationToken cancellationToken)
    {
        return await context.Types.Find(x=>x.Id==productTypeId).FirstOrDefaultAsync(cancellationToken);
    }
}