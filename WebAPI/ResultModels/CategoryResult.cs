using WebAPI.DTOs.Categories;

namespace WebAPI.ResultModels
{
    public class CategoryResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
