using AutoMapper;
using WebAPI.DTOs.Products;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class ProductsMP : Profile
    {
        public ProductsMP() {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO,  Product>();
        }
    }
}
