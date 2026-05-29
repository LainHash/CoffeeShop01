using AutoMapper;
using WebAPI.DTOs.Products;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class ProductsMP : Profile
    {
        public ProductsMP() {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO,  Product>();
        }
    }
}
