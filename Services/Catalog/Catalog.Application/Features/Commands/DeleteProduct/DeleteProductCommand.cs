using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommand(string id):IRequest<Result<bool>>
{
    public string Id { get; set; } = id;
}