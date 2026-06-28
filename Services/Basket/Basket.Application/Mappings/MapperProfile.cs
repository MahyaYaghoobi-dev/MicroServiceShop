using Basket.Application.DTOs;
using Basket.Core.Entities;
using Mapster;

namespace Basket.Application.Mappings;

public class MapperProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCart, ShoppingCartDto>()
            .Ignore(dest => dest.TotalPrice);  

        config.NewConfig<ShoppingCartItem, ShoppingCartItemDto>();
        config.NewConfig<BasketCheckout, BasketCheckoutDto>();
    }
}