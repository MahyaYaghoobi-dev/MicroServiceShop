using Catalog.Application.DTOs;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductTypes;

public class GetAllProductTypesQueryHandler(ITypeRepository typeRepository,IMapper mapper):IRequestHandler<GetAllProductTypesQuery,IEnumerable<TypeDto>>
{
    public async Task<IEnumerable<TypeDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        var types =await typeRepository.GetAllProductTypesAsync(cancellationToken);
        return mapper.Map<IEnumerable<TypeDto>>(types);
    }
}