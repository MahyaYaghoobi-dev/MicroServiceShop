using Basket.Application.DTOs;
using Basket.Application.Features.Commands.UpdateBasket;
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
        
        config.NewConfig<UpdateBasketCommand, ShoppingCart>()
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Items, src => src.Items)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
    }
}