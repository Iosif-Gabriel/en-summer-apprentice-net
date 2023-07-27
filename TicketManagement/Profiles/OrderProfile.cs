using AutoMapper;
using TicketManagement.Models;
using TicketManagement.Models.Dto;
namespace TicketManagement.Profiles
{
    public class OrderProfile:Profile
    {

        public OrderProfile() 
        {
            CreateMap<OrderU, OrderDto>()
             .ForMember(dest => dest.IdOrder, opt => opt.MapFrom(src => src.IdOrder))
             .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
             .ForMember(dest => dest.IdTicketCategory, opt => opt.MapFrom(src => src.IdTicketCategory))
             .ForMember(dest => dest.OrderedAt, opt => opt.MapFrom(src => src.OrderedAt))
             .ForMember(dest => dest.NumberOfTickets, opt => opt.MapFrom(src => src.NumberOfTickets))
             .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
             .ForMember(dest => dest.IdTicketCategoryNavigation, opt => opt.MapFrom(src => src.IdTicketCategoryNavigation.IdTicketCategory))
             .ForMember(dest => dest.IdUserNavigation, opt => opt.MapFrom(src => src.IdUserNavigation.IdUser));

          CreateMap<OrderPatchDto, OrderU>()
         .ForMember(dest => dest.IdOrder, opt => opt.MapFrom(src => src.IdOrderPatch)) // Custom mapping for IdOrder
         .ForMember(dest => dest.IdTicketCategory, opt => opt.MapFrom(src => src.IdTicketCategoryPatch)) // Custom mapping for IdTicketCategory
         .ForMember(dest => dest.NumberOfTickets, opt => opt.MapFrom(src => src.NumberOfTicketsPatch)) // Custom mapping for NumberOfTickets
         .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPricePatch)); // Custom mapping for TotalPrice
        }


        
    }
}
