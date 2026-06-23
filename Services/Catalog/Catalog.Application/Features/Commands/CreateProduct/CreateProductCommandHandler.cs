using Catalog.Application.DTOs;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper):IRequestHandler<CreateProductCommand,ProductDto?>
{
    public async Task<ProductDto?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<CreateProductCommand, Product>(request);
        var result = await productRepository.CreateProductAsync(product, cancellationToken);
        return result is null ? null : mapper.Map<ProductDto>(result);
    }
}