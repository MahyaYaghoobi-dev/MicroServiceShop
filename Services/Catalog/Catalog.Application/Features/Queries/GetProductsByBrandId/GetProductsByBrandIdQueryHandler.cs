using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByBrandId;

public class GetProductsByBrandIdQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetProductsByBrandIdQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetProductsByBrandIdQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByBrandIdAsync(request.Id, cancellationToken);
        return Result<IEnumerable<ProductDto>>.Success(mapper.Map<IEnumerable<ProductDto>>(products));
    }
}