using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByTypeId;

public class GetProductsByTypeIdQuery(string id) : IRequest<Result<IEnumerable<ProductDto>>>
{
    public string Id { get; set; } = id;
}