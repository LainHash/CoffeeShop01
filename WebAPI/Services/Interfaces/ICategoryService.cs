using WebAPI.DTOs.Categories;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResult<List<CategoryDTO>>> GetAllAsync();
    }
}
