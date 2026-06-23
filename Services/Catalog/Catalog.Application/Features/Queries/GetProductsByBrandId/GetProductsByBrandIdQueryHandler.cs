using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByBrandId;

public class GetProductsByBrandIdQueryHandler(IProductRepository productRepository,IMapper mapper):IRequestHandler<GetProductsByBrandIdQuery,IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByBrandIdQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByBrandIdAsync(request.Id,cancellationToken);
        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}