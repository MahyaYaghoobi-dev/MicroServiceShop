using Basket.Application.DTOs;
using Basket.Application.Shared.Results;
using MediatR;

namespace Basket.Application.Features.Commands.UpdateBasket;

public class UpdateBasketCommand(
    string userName,
    List<ShoppingCartItemDto> items) : IRequest<Result<ShoppingCartDto?>>
{
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItemDto> Items { get; set; } = items;
}