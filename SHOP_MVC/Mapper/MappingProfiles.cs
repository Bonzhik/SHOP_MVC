using AutoMapper;
using SHOP_MVC.Models;
using SHOP_MVC.Models.Dto;

namespace SHOP_MVC.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
