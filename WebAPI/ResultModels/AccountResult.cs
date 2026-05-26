namespace WebAPI.ResultModels
{
    public class AccountResult : BaseResult
    {
    }
    public class AccountResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
