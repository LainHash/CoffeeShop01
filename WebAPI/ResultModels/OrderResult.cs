using System.Diagnostics.Eventing.Reader;
using WebAPI.DTOs.Orders;

namespace WebAPI.ResultModels
{
    public class OrderResult : BaseResult
    {

    }
    public class OrderResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
