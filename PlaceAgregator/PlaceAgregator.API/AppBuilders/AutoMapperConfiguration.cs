﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.Shared.DTOs.Booking;
using PlaceAgregator.Shared.DTOs.Comments;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Types;

namespace PlaceAgregator.API.AppBuilders
{
    public static class AutoMapperConfigurator
    {
        public static IMapperConfigurationExpression ConfigureAutoMapper(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Place, PlaceCardInfo>();
            cfg.CreateMap<PlaceCreateDTO, Place>();
            cfg.CreateMap<PlaceUpdateDTO, Place>()
                .ForMember(dest => dest.EventTypes,
                    opt => opt.MapFrom(src => src.EventTypeIds.Select(item => new EventType() { Id = item })))
                .ForMember(dest => dest.Prohibitions,
                    opt => opt.MapFrom(src => src.ProhibitionIds.Select(item => new Prohibition() { Id = item })));

            cfg.CreateMap<Place, GetPlaceDTO>()
                .ForMember(dest => dest.EventTypeIds,
                    opt => opt.MapFrom(src => src.EventTypes.Select(item => item.Id)))
                .ForMember(dest => dest.ProhibitionIds,
                    opt => opt.MapFrom(src => src.Prohibitions.Select(item => item.Id)));

            cfg.CreateMap<SheduleDTO, Shedule>();
            cfg.CreateMap<Shedule, SheduleDTO>();

            cfg.CreateMap<Charge, ChargeGetDTO>();
            cfg.CreateMap<ChargeCreateDTO, Charge>();

            cfg.CreateMap<Discount, DiscountGetDTO>();
            cfg.CreateMap<DiscountCreateDTO, Discount>();

            cfg.CreateMap<ServiceItem, ServiceItemGetDTO>();
            cfg.CreateMap<ServiceItemCreateDTO, ServiceItem>();
            cfg.CreateMap<ServiceItemUpdateDTO, ServiceItem>();

            cfg.CreateMap<BookingRequest, BookingRequestGetDTO>();
            cfg.CreateMap<BookingRequestCreateDTO, BookingRequest>();
            cfg.CreateMap<BookingRequestServiceItemDTO, BookingRequestServiceItem>();
            cfg.CreateMap<BookingRequestServiceItem, BookingRequestServiceItemGetDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ServiceItem.Title));

            cfg.CreateMap<CommentDTO, Comment>();
            cfg.CreateMap<Comment, CommentGetDTO>();

            return cfg;
        }
    }
}