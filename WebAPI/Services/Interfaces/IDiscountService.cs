using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountResult> GetAllAsync();
        Task<DiscountResult> GetOneAsync(int id);
    }
}
