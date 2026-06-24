using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductTypes;

public class GetAllProductTypesQuery : IRequest<Result<IEnumerable<TypeDto>>>
{
}