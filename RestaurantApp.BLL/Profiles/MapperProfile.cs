using AutoMapper;
using RestaurantApp.BLL.Dtos.MenuItemDtos;
using RestaurantApp.BLL.Dtos.OrderDtos;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Profiles
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<MenuItemCreateDto, MenuItem>();
            CreateMap<MenuItemUpdateDto, MenuItem>();
            CreateMap<MenuItem, MenuItemReturnDto>();

            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.MenuItemCount,
                    opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Count)));
            CreateMap<Order, OrderDetailReturnDto>()
                .ForMember(dest => dest.MenuItemCount,
                    opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Count)));

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(dest => dest.MenuItemNumber,
                    opt => opt.MapFrom(src => src.MenuItem.Number))
                .ForMember(dest => dest.MenuItemName,
                    opt => opt.MapFrom(src => src.MenuItem.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.MenuItem.Price));
        }
    }
}
