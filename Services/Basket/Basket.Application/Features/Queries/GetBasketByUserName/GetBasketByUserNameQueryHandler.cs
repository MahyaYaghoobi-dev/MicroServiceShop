using Basket.Application.DTOs;
using Basket.Application.Shared.Results;
using Basket.Core;
using MapsterMapper;
using MediatR;

namespace Basket.Application.Features.Queries.GetBasketByUserName;

public class GetBasketByUserNameQueryHandler(
    IBasketRepository basketRepository,
    IMapper mapper)
    : IRequestHandler<GetBasketByUserNameQuery, Result<ShoppingCartDto>>
{
    public async Task<Result<ShoppingCartDto>> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasketAsync(request.UserName, cancellationToken);

        if (basket is null)
            return Result<ShoppingCartDto>.Failure($"Basket for user '{request.UserName}' not found.", ResultType.NotFound);

        return Result<ShoppingCartDto>.Success(mapper.Map<ShoppingCartDto>(basket));
    }
}