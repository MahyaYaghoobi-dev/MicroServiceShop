using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByTypeId;

public class GetProductsByTypeIdQueryHandler(IProductRepository productRepository , IMapper mapper):IRequestHandler<GetProductsByTypeIdQuery,IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByTypeIdQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByTypeIdAsync(request.Id, cancellationToken);
       return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}