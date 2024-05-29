using AutoMapper;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Auth;
using ModelsLibrary.DtoModels.Order;
using ModelsLibrary.DtoModels.Product;
using ModelsLibrary.DtoModels.ShoppingCart;
using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.DtoModels.User;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, BuyerRegisterDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, BuyerStoreDto>().ReverseMap();
            CreateMap<Store, BuyerStoreLocationDto>().ReverseMap();
            CreateMap<Store, UpdateStoreDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(
                    dest => dest.UserEmail,
                    opt => opt.MapFrom(src => src.User.Email)
                )
                .ForMember(
                    dest => dest.StoreName,
                    opt => opt.MapFrom(src => src.Store.Name)
                )
                .ReverseMap();
            CreateMap<Order, OrderForExcelDto>()
              .ForMember(
                  dest => dest.User,
                  opt => opt.MapFrom(src => src.User.Email)
              )
              .ForMember(
                  dest => dest.Store,
                  opt => opt.MapFrom(src => src.Store.Name)
              )
              .ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();

        }
    }
}
