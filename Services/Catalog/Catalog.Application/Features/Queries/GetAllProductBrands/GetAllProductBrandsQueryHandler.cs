using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductBrands;

public class GetAllProductBrandsQueryHandler(IMapper mapper, IBrandRepository  brandRepository):IRequestHandler<GetAllProductBrandsQuery,IEnumerable<BrandDto>>
{
    public async Task<IEnumerable<BrandDto>> Handle(GetAllProductBrandsQuery request,
        CancellationToken cancellationToken)
    {
        var brands = await brandRepository.GetAllProductBrandsAsync(cancellationToken);
        return mapper.Map<IEnumerable<BrandDto>>(brands);
    }
}