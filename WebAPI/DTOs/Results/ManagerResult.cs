using WebAPI.DTOs.Accounts.Managers;

namespace WebAPI.DTOs.Results
{
    public class ManagerResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ManagerDTO? Manager { get; set; }

        public ManagerResult(bool succes, string message, ManagerDTO? manager = null)
        {
            Success = succes;
            Message = message;
            Manager = manager;
        }
    }
}
