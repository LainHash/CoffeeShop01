using WebAPI.ResultModels;

namespace WebAPI.ResultModels
{
    public class AuthResult<T> : BaseResult
    {
        public T? Data { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
