using WebAPI.DTOs.TableEntities;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface ITableService
    {
        Task<TableResult<List<TableEntityDTO>>> GetAllAsync();
        Task<TableResult<TableEntityDTO>> GetOneAsync(int floorNumber, int tableNumber);
        Task<TableResult<List<TableEntityDTO>>> GetAllByFloorAsync(int floorNumber);
        Task<TableResult<TableEntityDTO>> UpdateStatusAsync(int tableId, string status);
    }
}
