using AutoMapper;
using WebAPI.DTOs.Categories;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CategoryMP : Profile
    {
        public CategoryMP()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
