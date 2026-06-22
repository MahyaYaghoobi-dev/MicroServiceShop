namespace Catalog.Application.DTOs;

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    
    public BrandDto Brand{get; set;}
    public TypeDto  Type { get; set; }
}