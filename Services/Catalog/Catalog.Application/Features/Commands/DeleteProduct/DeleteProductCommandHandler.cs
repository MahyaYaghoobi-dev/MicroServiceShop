using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository):IRequestHandler<DeleteProductCommand,bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
       return await productRepository.DeleteProductAsync(request.Id,cancellationToken);
    }
}