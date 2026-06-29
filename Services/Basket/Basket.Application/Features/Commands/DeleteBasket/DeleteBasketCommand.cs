using Basket.Application.Shared.Results;
using MediatR;

namespace Basket.Application.Features.Commands.DeleteBasket;

public class DeleteBasketCommand(string userName) : IRequest<Result<bool>>
{
    public string UserName { get; set; } = userName;
}