using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository,IMapper mapper):IRequestHandler<UpdateProductCommand,bool>
{
    public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        return productRepository.UpdateProductAsync(product, cancellationToken);
    }
}