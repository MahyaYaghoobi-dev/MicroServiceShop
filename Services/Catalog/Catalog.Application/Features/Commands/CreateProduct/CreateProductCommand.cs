using Catalog.Application.DTOs;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Features.Commands.CreateProduct;

public class CreateProductCommand(
    string name,
    string summary,
    string description,
    string imageFile,
    decimal price,
    ProductBrand brand,
    ProductType type)
    : IRequest<ProductDto>
{
    public string Name { get; set; } = name;
    public string Summary { get; set; } = summary;
    public string Description { get; set; } = description;
    public string ImageFile { get; set; } = imageFile;
    public decimal Price { get; set; } = price;


    public ProductBrand Brand { get; set; } = brand;
    public ProductType Type { get; set; } = type;
}