using Basket.Application.Shared.Results;
using MediatR;

namespace Basket.Application.Features.Commands.DeleteItemFromBasket;

public class DeleteItemFromBasketCommand(
    string userName,
    string productId,
    DateTime lastUpdated) : IRequest<Result<bool>>
{
    public string UserName { get; set; } = userName;
    public string ProductId { get; set; } = productId;
    public DateTime LastUpdated { get; set; } = lastUpdated;
}