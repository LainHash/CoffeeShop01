using BlazorApp.Models.Discounts;

namespace BlazorApp.Services.Interfaces
{
    public interface IDiscountApiService
    {
        Task<DiscountListResponse?> GetAllAsync();
    }
}
