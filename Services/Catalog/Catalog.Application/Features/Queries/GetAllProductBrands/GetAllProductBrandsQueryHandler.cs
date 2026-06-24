using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductBrands;

public class GetAllProductBrandsQueryHandler(
    IMapper mapper,
    IBrandRepository brandRepository)
    : IRequestHandler<GetAllProductBrandsQuery, Result<IEnumerable<BrandDto>>>
{
    public async Task<Result<IEnumerable<BrandDto>>> Handle(
        GetAllProductBrandsQuery request,
        CancellationToken cancellationToken)
    {
        var brands = await brandRepository.GetAllProductBrandsAsync(cancellationToken);
        return Result<IEnumerable<BrandDto>>.Success(mapper.Map<IEnumerable<BrandDto>>(brands));
    }
}