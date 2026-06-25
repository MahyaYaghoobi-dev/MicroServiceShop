using Catalog.Application.DTOs;
using Catalog.Application.Features.Queries.GetAllProducts;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
{
    public async Task<PaginatedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var specParams = new ProductSpecParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            SortBy = request.SortBy,
            SortOrder = request.SortOrder ?? SortOrder.Asc,  // ← مقدار پیش‌فرض
            BrandId = request.BrandId,
            TypeId = request.TypeId
        };

        var products = await productRepository.GetAllProductsAsync(specParams, cancellationToken);
        var count = await productRepository.GetProductsCountAsync(specParams, cancellationToken);

        return new PaginatedResult<ProductDto>(
            specParams.PageIndex,
            specParams.PageSize,
            count,
            mapper.Map<IEnumerable<ProductDto>>(products)
        );
    }
}