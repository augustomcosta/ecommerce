using AutoMapper;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Dtos;

namespace ecommerce_api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>();
    }
    
    
}