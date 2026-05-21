using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ITableService
    {
        Task<TableResult> GetAllAsync();
        Task<TableResult> GetOneAsync(int floorNumber, int tableNumber);
        Task<TableResult> GetAllByFloorAsync(int floorNumber);
    }
}
