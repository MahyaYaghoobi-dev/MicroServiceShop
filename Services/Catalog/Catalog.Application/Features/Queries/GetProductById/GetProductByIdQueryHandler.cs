using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository,IMapper mapper):IRequestHandler<GetProductByIdQuery,ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id,cancellationToken);
        return product is null ? null : mapper.Map<ProductDto>(product);
        
    }
}