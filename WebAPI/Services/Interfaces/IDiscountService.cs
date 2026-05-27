using WebAPI.DTOs.Discounts;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountResult<List<DiscountDTO>>> GetAllAsync();
        Task<DiscountResult<DiscountDTO>> GetOneAsync(string code);
    }
}
