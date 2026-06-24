using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository):IRequestHandler<DeleteProductCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
       var result= await productRepository.DeleteProductAsync(request.Id,cancellationToken);

       if (!result)
           return Result<bool>.Failure("Faild to delete product", ResultType.BadRequest);
       
       return Result<bool>.Success(true);
    }
}