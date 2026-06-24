using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetProductsByName;

public class GetProductsByNameQuery(string name) : IRequest<Result<IEnumerable<ProductDto>>>
{
    public string Name { get; set; } = name;
}