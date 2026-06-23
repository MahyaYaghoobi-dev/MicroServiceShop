using MediatR;

namespace Catalog.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommand(string id):IRequest<bool>
{
    public string Id { get; set; } = id;
}