using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByTypeId;

public class GetProductsByTypeIdQuery(string id) : IRequest<IEnumerable<ProductDto>>
{
    public string Id { get; set; }
}