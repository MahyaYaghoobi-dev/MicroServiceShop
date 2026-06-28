using Basket.Application.DTOs;
using Basket.Application.Shared.Results;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Basket.Application.Features.Commands.UpdateBasket;

public class UpdateBasketCommandHandler(
    IMapper mapper,
    IBasketRepository basketRepository)
    : IRequestHandler<UpdateBasketCommand, Result<ShoppingCartDto?>>
{
    public async Task<Result<ShoppingCartDto?>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var items = mapper.Map<List<ShoppingCartItem>>(request.Items);
        var entity = new ShoppingCart(request.UserName) { Items = items };
        var result = await basketRepository.UpdateBasketAsync(entity, cancellationToken);

        if (result is null)
            return Result<ShoppingCartDto>.Failure("Failed to update basket.", ResultType.BadRequest);

        return Result<ShoppingCartDto>.Success(mapper.Map<ShoppingCartDto>(result));
    }
}