using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Results;
using WebAPI.DTOs.TableEntities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class TableService : ITableService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public TableService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TableResult> GetAllAsync()
        {
            var tables = await _context.TableEntities
                .ToListAsync();
            return new TableResult(true, "Lấy danh sách bàn thành công", _mapper.Map<List<TableEntityDTO>>(tables));
        }

        public async Task<TableResult> GetOneAsync(int floorNumber, int tableNumber)
        {
            var table = await _context.TableEntities
                .FirstOrDefaultAsync(t => t.TableNumber == tableNumber && t.FloorNumber == floorNumber);
            if(table == null)
            {
                return new TableResult(false, "Bàn không tồn tại!");
            }
            return new TableResult(true, "Lấy bàn thành công.", _mapper.Map<TableEntityDTO>(table));
        }

        public async Task<TableResult> GetAllByFloorAsync(int floorNumber)
        {
            var tables = await _context.TableEntities
                .Where(t => t.FloorNumber == floorNumber)
                .ToListAsync();
            return new TableResult(true, "Lấy danh sách bàn theo tầng thành công", _mapper.Map<List<TableEntityDTO>>(tables));
        }

        public async Task<TableResult> UpdateStatusAsync(int tableId, string status)
        {
            var table = await _context.TableEntities.FindAsync(tableId);
            if (table == null)
            {
                return new TableResult(false, "Không tìm thấy bàn!");
            }

            // Có thể kiểm tra status có hợp lệ không (ví dụ kiểm tra danh sách hằng số),
            // nhưng tạm thời cập nhật trực tiếp.
            table.Status = status;
            _context.TableEntities.Update(table);
            await _context.SaveChangesAsync();

            return new TableResult(true, "Cập nhật trạng thái bàn thành công", _mapper.Map<TableEntityDTO>(table));
        }
    }
}
