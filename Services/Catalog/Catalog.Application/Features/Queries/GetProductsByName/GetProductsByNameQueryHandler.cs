using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByName;

public class GetProductsByNameQueryHandler(IProductRepository productRepository,IMapper mapper):IRequestHandler<GetProductsByNameQuery,IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByNameAsync(request.Name,cancellationToken);
        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}