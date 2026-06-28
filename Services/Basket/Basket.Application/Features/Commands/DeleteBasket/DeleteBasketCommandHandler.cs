using Basket.Application.Shared.Results;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.Commands.DeleteBasket;

public class DeleteBasketCommandHandler(IBasketRepository basketRepository):IRequestHandler<DeleteBasketCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.DeleteBasketAsync(request.UserName, cancellationToken);
    
        if (!result)
            return Result<bool>.Failure($"Basket for user '{request.UserName}' not found.", ResultType.NotFound);
    
        return Result<bool>.Success(true);
    }
    
}