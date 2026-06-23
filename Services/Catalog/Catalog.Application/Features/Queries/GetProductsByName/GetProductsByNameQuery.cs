using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByName;

public class GetProductsByNameQuery(string name):IRequest<IEnumerable<ProductDto>>
{
    public string Name { get; set; } = name;
}