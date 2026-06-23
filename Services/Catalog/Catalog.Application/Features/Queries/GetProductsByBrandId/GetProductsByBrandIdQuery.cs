using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByBrandId;

public class GetProductsByBrandIdQuery(string id):IRequest<IEnumerable<ProductDto>>
{
    public string Id { get; set; }
}