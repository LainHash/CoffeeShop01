using WebAPI.DTOs.Accounts.Customers;

namespace WebAPI.ResultModels
{
    public class CustomerResult : BaseResult
    {
    }
    public class CustomerResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
