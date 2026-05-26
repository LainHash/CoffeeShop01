using WebAPI.DTOs.Accounts.Managers;

namespace WebAPI.ResultModels
{
    public class ManagerResult : BaseResult
    {

    }
    public class ManagerResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
