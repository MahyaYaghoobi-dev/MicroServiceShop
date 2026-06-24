using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByTypeId;

public class GetProductsByTypeIdQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetProductsByTypeIdQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetProductsByTypeIdQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByTypeIdAsync(request.Id, cancellationToken);
        return Result<IEnumerable<ProductDto>>.Success(mapper.Map<IEnumerable<ProductDto>>(products));
    }
}