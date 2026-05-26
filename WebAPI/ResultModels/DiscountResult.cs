using WebAPI.DTOs.Discounts;

namespace WebAPI.ResultModels
{
    public class DiscountResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
