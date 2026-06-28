using Basket.Application.DTOs;
using Basket.Application.Shared.Results;
using MediatR;

namespace Basket.Application.Features.Queries.GetBasketByUserName;

public class GetBasketByUserNameQuery(String userName):IRequest<Result<ShoppingCartDto>>
{
    public string UserName { get; set; } = userName;
}