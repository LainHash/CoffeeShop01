namespace WebAPI.DTOs.Results
{
    public class ResultBase
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ResultBase(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
