using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllProductsAsync(cancellationToken);
        return Result<IEnumerable<ProductDto>>.Success(mapper.Map<IEnumerable<ProductDto>>(products));
    }
}