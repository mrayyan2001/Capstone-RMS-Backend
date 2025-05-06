using api.DTOs.Bookmark;
using api.DTOs.Offers;
using api.Models;
using AutoMapper;

namespace api.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Bookmark, FavItemDTOs>()
            .ForMember(dest => dest.ItemNameEn, opt => opt.MapFrom(src => src.Item.ItemNameEn))
            .ForMember(dest => dest.ItemDescriptionEn, opt => opt.MapFrom(src => src.Item.ItemDescriptionEn))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item.Price))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId));

            CreateMap<AddItemToFavDTO, Bookmark>();
            CreateMap<IEnumerable<Offer>, OffersDTOs>();


        }
    }
}
