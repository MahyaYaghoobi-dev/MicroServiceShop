using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository productRepository,IMapper mapper):IRequestHandler<GetAllProductsQuery,IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products =await productRepository.GetAllProductsAsync(cancellationToken);
        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}