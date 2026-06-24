using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id, cancellationToken);
        
        if (product is null)
            return Result<ProductDto>.Failure($"Product with id '{request.Id}' not found.", ResultType.NotFound);
        
        return Result<ProductDto>.Success(mapper.Map<ProductDto>(product));
    }
}