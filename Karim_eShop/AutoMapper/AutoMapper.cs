using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Data.Entity.Model;
using AutoMapper;

namespace Karim_eShop.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
        }
    }
}
