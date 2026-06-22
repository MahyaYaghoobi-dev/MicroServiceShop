using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductBrands;

public class GetAllProductBrandsQuery:IRequest<IEnumerable<BrandDto>>
{
    
}
