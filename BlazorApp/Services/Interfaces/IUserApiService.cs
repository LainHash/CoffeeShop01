using System.Threading.Tasks;
using BlazorApp.Models.Managers;
using BlazorApp.Models.Customers;

namespace BlazorApp.Services.Interfaces
{
    public interface IUserApiService
    {
        Task<EmployeeListResponse?> GetEmployeeListAsync();
        Task<CustomerListResponse?> GetCustomerListAsync();
    }
}
