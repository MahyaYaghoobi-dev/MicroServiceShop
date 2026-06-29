using Basket.Api.Helpers;
using Basket.Application.Features.Commands.DeleteBasket;
using Basket.Application.Features.Commands.DeleteItemFromBasket;
using Basket.Application.Features.Commands.UpdateBasket;
using Basket.Application.Features.Queries.GetBasketByUserName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

public class BasketController(IMediator mediator) : ApiController
{
    [HttpGet("{userName}")]
    public async Task<IActionResult> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBasketByUserNameQuery(userName), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteBasketCommand(userName), cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{userName}/items/{productId}")]
    public async Task<IActionResult> DeleteItem(
        string userName,
        string productId,
        [FromBody] DateTime lastUpdated,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new DeleteItemFromBasketCommand(userName, productId, lastUpdated),
            cancellationToken);

        return result.ToActionResult();
    }
}