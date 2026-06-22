using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProductTypes;

public class GetAllProductTypesQuery:IRequest<IEnumerable<TypeDto>>
{
}