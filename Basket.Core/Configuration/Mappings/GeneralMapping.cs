using AutoMapper;
using Basket.Models.Dtos;
using Shared.Messages.Events.Concrete;

namespace Basket.Core.Configuration.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Models.Basket, BasketDto>()
                .ForMember(dto => dto.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
                .ReverseMap();
            CreateMap<Models.Models.BasketItem, BasketItemDto>()
                .ReverseMap();
            CreateMap<Models.Request.BasketRequest, Models.Models.Basket>()
                .ForMember(dto => dto.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
                .ReverseMap();
            CreateMap<Models.Request.BasketItemRequest, Models.Models.BasketItem>()
                .ReverseMap();
            CreateMap<BasketCheckoutDto, BasketCheckoutEvent>()
                .ReverseMap();
        }
    }
}
