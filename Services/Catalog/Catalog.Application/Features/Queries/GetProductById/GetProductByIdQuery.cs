using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductById;

public class GetProductByIdQuery(string id):IRequest<ProductDto>
{
    public string Id { get; set; } = id;

}