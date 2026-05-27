using BlazorApp.Models.Tables;

namespace BlazorApp.Services.Interfaces
{
    public interface ITableApiService
    {
        Task<TableListResponse?> GetAllAsync();
        Task<TableResponse?> UpdateStatusAsync(int floorNumber, int tableNumber, string status);
    }
}
