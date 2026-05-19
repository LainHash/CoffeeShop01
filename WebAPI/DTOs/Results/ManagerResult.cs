using WebAPI.DTOs.Accounts.Managers;

namespace WebAPI.DTOs.Results
{
    public class ManagerResult : ResultBase
    {
        public ManagerDTO? Manager { get; set; }

        public ManagerResult(bool succes, string message, ManagerDTO? manager = null) : base(succes, message)
        {
            Manager = manager;
        }
    }
}
