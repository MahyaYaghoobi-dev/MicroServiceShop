using Basket.Application.Shared.Results;
using Basket.Core;
using MediatR;

namespace Basket.Application.Features.Commands.DeleteItemFromBasket;

public class DeleteItemFromBasketCommandHandler(
    IBasketRepository basketRepository)
    : IRequestHandler<DeleteItemFromBasketCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.DeleteItemAsync(
            request.UserName,
            request.ProductId,
            request.LastUpdated,
            cancellationToken);

        if (!result)
            return Result<bool>.Failure($"Item with product id '{request.ProductId}' not found.", ResultType.NotFound);

        return Result<bool>.Success(true);
    }
}