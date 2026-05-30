using System.Threading.Tasks;
using BlazorApp.Models.Managers;
using BlazorApp.Models.Customers;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class UserApiService : IUserApiService
    {
        private readonly IApiService _apiService;

        public UserApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<EmployeeListResponse?> GetEmployeeListAsync()
        {
            return await _apiService.GetAsync<EmployeeListResponse>("/api/User/Employees");
        }

        public async Task<CustomerListResponse?> GetCustomerListAsync()
        {
            return await _apiService.GetAsync<CustomerListResponse>("/api/User/Customers");
        }
    }
}
