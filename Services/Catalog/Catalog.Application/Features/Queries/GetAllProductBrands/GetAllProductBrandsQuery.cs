using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductBrands;

public class GetAllProductBrandsQuery : IRequest<Result<IEnumerable<BrandDto>>>
{
}