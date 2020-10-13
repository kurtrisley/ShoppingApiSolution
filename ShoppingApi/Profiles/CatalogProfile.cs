using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Catalog;
using ShoppingApi.Models.Catalog.Curbside;

namespace ShoppingApi.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile(ConfigurationForMapper config)
        {
            CreateMap<ShoppingItem, GetCatalogResponseSummaryItem>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => 
                src.Cost * config.markUp));

            CreateMap<PostCatalogRequest, ShoppingItem>()
                .ForMember(dest => dest.InInventory, opt => opt.MapFrom(src => true));

            CreateMap<PostCurbsideOrderRequest, CurbsideOrder>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => CurbsideOrderStatus.Pending));
        }
    }
}
