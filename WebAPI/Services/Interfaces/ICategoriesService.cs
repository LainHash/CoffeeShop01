using WebAPI.DTOs.Categories;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoryDTO>> GetAllAsync();
    }
}
