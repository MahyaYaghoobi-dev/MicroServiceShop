using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery:IRequest<IEnumerable<ProductDto>>
{
    
}