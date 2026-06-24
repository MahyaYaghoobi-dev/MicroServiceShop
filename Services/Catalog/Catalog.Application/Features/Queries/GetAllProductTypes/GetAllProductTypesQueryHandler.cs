using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductTypes;

public class GetAllProductTypesQueryHandler(
    ITypeRepository typeRepository,
    IMapper mapper)
    : IRequestHandler<GetAllProductTypesQuery, Result<IEnumerable<TypeDto>>>
{
    public async Task<Result<IEnumerable<TypeDto>>> Handle(
        GetAllProductTypesQuery request,
        CancellationToken cancellationToken)
    {
        var types = await typeRepository.GetAllProductTypesAsync(cancellationToken);
        return Result<IEnumerable<TypeDto>>.Success(mapper.Map<IEnumerable<TypeDto>>(types));
    }
}