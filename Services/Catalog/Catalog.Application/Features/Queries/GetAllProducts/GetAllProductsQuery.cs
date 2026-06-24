using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>
{
}