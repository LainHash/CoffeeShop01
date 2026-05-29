using BlazorApp.Models.Tables;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class TableApiService : ITableApiService
    {
        private readonly IApiService _apiService;

        public TableApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<TableListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<TableListResponse>("/api/Table");
        }

        public async Task<TableResponse?> UpdateStatusAsync(int floorNumber, int tableNumber, string status)
        {
            var payload = new { Status = status };
            return await _apiService.PutAsync<object, TableResponse>($"/api/Table/{floorNumber}/{tableNumber}/status", payload);
        }
    }
}
