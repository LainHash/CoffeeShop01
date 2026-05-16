using WebAPI.DTOs.Categories;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
    }
}
