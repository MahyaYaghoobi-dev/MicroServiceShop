using Catalog.Application.DTOs;
using Catalog.Application.Features.Queries.GetAllProductBrands;
using Catalog.Application.Features.Queries.GetAllProducts;
using Catalog.Application.Features.Queries.GetAllProductTypes;
using Catalog.Core.Entities;
using Mapster;

namespace Catalog.Application.Mappings;

public class MapperProfile:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        
        //dtos config
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Brand, src => src.Brand)
            .Map(dest => dest.Type, src => src.Type);
        
        config.NewConfig<ProductBrand, BrandDto>();
        config.NewConfig<ProductType, TypeDto>();

    }
}