using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Features.Commands.CreateProduct;

public class CreateProductCommand(
    string name,
    string summary,
    string description,
    string imageFile,
    decimal price,
    string brandId,
    string typeId)
    : IRequest<Result<ProductDto>>
{
    public string Name { get; set; } = name;
    public string Summary { get; set; } = summary;
    public string Description { get; set; } = description;
    public string ImageFile { get; set; } = imageFile;
    public decimal Price { get; set; } = price;


    public string BrandId { get; set; } = brandId;
    public string TypeId { get; set; } = typeId;
}