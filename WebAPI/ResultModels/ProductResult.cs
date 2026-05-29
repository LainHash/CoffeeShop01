using WebAPI.DTOs.Products;

namespace WebAPI.ResultModels
{
    public class ProductResult : BaseResult
    {
    }
    public class ProductResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
