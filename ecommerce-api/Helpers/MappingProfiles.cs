﻿using AutoMapper;
using Domain.Models;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Models.Identity;
using ecommerce_api.Dtos;

namespace ecommerce_api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.Brand,
                o =>
                    o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.Category, o =>
                o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.ImageUrl, o =>
                o.MapFrom<ProductUrlResolver>());
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();
        CreateMap<AddressDto, Domain.Models.OrderAggregate.Address>();
    }
    
    
}