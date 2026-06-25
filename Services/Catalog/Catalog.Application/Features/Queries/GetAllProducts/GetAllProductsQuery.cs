using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery(
    int pageIndex = 1,
    int pageSize = 10,
    string? search = null,
    string? sortBy = null,
    SortOrder? sortOrder = null,
    string? brandId = null,
    string? typeId = null)
    : IRequest<PaginatedResult<ProductDto>>
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public string? Search { get; } = search;
    public string? SortBy { get; } = sortBy;
    public SortOrder? SortOrder { get; } = sortOrder;
    public string? BrandId { get; } = brandId;
    public string? TypeId { get; } = typeId;
}