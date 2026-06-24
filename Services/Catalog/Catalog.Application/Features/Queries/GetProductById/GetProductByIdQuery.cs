using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductById;

public class GetProductByIdQuery(string id) : IRequest<Result<ProductDto>>
{
    public string Id { get; set; } = id;
}