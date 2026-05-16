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
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO,  Product>();
        }
    }
}
