using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByName;

public class GetProductsByNameQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetProductsByNameQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetProductsByNameQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByNameAsync(request.Name, cancellationToken);
        return Result<IEnumerable<ProductDto>>.Success(mapper.Map<IEnumerable<ProductDto>>(products));
    }
}