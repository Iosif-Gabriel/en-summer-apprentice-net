﻿using AutoMapper;
using TicketManagement.Models;
using TicketManagement.Models.Dto;

namespace TicketManagement.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventU, EventDto>()
             .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Idevent))
             .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
             .ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.DescriptionEvent))
             .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.IdEventTypeNavigation != null ? src.IdEventTypeNavigation.EventTypeName : string.Empty))
             .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.IdVenueNavigation != null ? src.IdVenueNavigation.VenueLocation:string.Empty));


            /*    CreateMap<Venue, VenueDto>()
                    .ForMember(dest => dest.IdVenue, opt => opt.MapFrom(src => src.IdVenue))
                    .ForMember(dest => dest.VenueLocation, opt => opt.MapFrom(src => src.VenueLocation))
                    .ForMember(dest => dest.VenueType, opt => opt.MapFrom(src => src.VenueType ?? string.Empty))
                    .ForMember(dest => dest.VenueCapacity, opt => opt.MapFrom(src => src.VenueCapacity ?? 0));*/

            CreateMap<EventU, EventPatchDto>();
        }
    }
}
