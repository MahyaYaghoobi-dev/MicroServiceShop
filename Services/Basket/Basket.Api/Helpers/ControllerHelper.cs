using Basket.Application.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Helpers;

public static class ControllerHelper
{

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Type switch
        {
            ResultType.Success => new OkObjectResult(result.Data),
            ResultType.NotFound => new NotFoundObjectResult(result.Error),
            ResultType.BadRequest => new BadRequestObjectResult(result.Error),
            ResultType.ValidationError => new BadRequestObjectResult(result.Error),
            ResultType.Conflict => new ConflictObjectResult(result.Error),
            ResultType.Unauthorized => new UnauthorizedObjectResult(result.Error),
            _ => new ObjectResult(result.Error) { StatusCode = 500 }
        };
    }
    
}