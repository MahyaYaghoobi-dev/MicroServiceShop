using Basket.Application.DTOs;
using Basket.Application.Shared.Results;
using Basket.Core.Entities;
using Basket.Core;
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
        var entity = mapper.Map<ShoppingCart>(request);

        var result = await basketRepository.UpdateBasketAsync(entity, request.LastUpdated, cancellationToken);

        if (result is null)
            return Result<ShoppingCartDto>.Failure("Failed to update basket.", ResultType.BadRequest);

        return Result<ShoppingCartDto>.Success(mapper.Map<ShoppingCartDto>(result));
    }
}