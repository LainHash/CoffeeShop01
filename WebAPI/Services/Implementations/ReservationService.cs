using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Reservations;
using WebAPI.DTOs.Results;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationResult> CreateAsync(CreateReservationDTO dto)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == dto.CustomerPublicId);

            if (customer == null)
            {
                return new ReservationResult(false, "Khách hàng không tồn tại.");
            }

            if (dto.ReservationTime <= DateTime.Now.AddMinutes(30))
            {
                return new ReservationResult(false, "Thời gian đặt bàn phải ít nhất 30 phút trong tương lai.");
            }

            if (dto.NumberOfGuests <= 0)
            {
                return new ReservationResult(false, "Số lượng khách phải lớn hơn 0.");
            }

            var reservation = new Reservation
            {
                CustomerId = customer.CustomerId,
                TableId = 0,
                ReservationTime = dto.ReservationTime,
                NumberOfGuests = dto.NumberOfGuests,
                Note = dto.Note,
                Status = "Pending",
            };

            // Tìm bàn phù hợp và KHÔNG bị trùng lịch trong khoảng ±2 giờ
            var conflictWindowStart = dto.ReservationTime.AddHours(-2);
            var conflictWindowEnd = dto.ReservationTime.AddHours(2);

            var busyTableIds = await _context.Reservations
                .Where(r => r.Status != "Cancelled"
                         && r.ReservationTime >= conflictWindowStart
                         && r.ReservationTime <= conflictWindowEnd)
                .Select(r => r.TableId)
                .Distinct()
                .ToListAsync();

            var availableTable = await _context.TableEntities
                .Where(t => t.IsActive == true
                         && t.Status == "Available"
                         && t.RecommendedCapacity >= dto.NumberOfGuests
                         && !busyTableIds.Contains(t.TableId))
                .OrderBy(t => t.RecommendedCapacity)
                .FirstOrDefaultAsync();

            if (availableTable == null)
                return new ReservationResult(false, "Hiện không có bàn trống phù hợp với số lượng khách trong khung giờ này. Vui lòng chọn giờ khác.");

            reservation.TableId = availableTable.TableId;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            await _context.Entry(reservation).Reference(r => r.Customer).LoadAsync();
            await _context.Entry(reservation.Customer).Reference(c => c.User).LoadAsync();
            await _context.Entry(reservation).Reference(r => r.Table).LoadAsync();

            return new ReservationResult(true, "Đặt bàn thành công! Chúng tôi sẽ xác nhận sớm.", _mapper.Map<ReservationDTO>(reservation));
        }

        public async Task<ReservationResult> GetByCustomerAsync(Guid customerPublicId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == customerPublicId);

            if (customer == null)
                return new ReservationResult(false, "Khách hàng không tồn tại.");

            var reservations = await _context.Reservations
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Table)
                .Where(r => r.CustomerId == customer.CustomerId)
                .OrderByDescending(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult(true, "Lấy danh sách đặt bàn thành công.",
                reservations: _mapper.Map<List<ReservationDTO>>(reservations));
        }

        public async Task<ReservationResult> GetAllAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Table)
                .OrderByDescending(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult(true, "Lấy tất cả đặt bàn thành công.",
                reservations: _mapper.Map<List<ReservationDTO>>(reservations));
        }

        public async Task<ReservationResult> GetByIdAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
                return new ReservationResult(false, "Đặt bàn không tồn tại.");

            return new ReservationResult(true, "Lấy thông tin đặt bàn thành công.", _mapper.Map<ReservationDTO>(reservation));
        }

        public async Task<ReservationResult> UpdateAsync(int reservationId, UpdateReservationDTO dto)
        {
            var allowedStatuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
            if (!allowedStatuses.Contains(dto.Status))
                return new ReservationResult(false, $"Trạng thái không hợp lệ. Giá trị hợp lệ: {string.Join(", ", allowedStatuses)}.");

            var reservation = await _context.Reservations
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
                return new ReservationResult(false, "Đặt bàn không tồn tại.");

            if (dto.TableId.HasValue)
            {
                var table = await _context.TableEntities
                    .FirstOrDefaultAsync(t => t.TableId == dto.TableId.Value && t.IsActive == true);

                if (table == null)
                    return new ReservationResult(false, "Bàn không tồn tại hoặc không hoạt động.");

                reservation.TableId = dto.TableId.Value;
            }

            if (dto.Note != null)
                reservation.Note = dto.Note;

            reservation.Status = dto.Status;
            await _context.SaveChangesAsync();

            await _context.Entry(reservation).Reference(r => r.Table).LoadAsync();

            return new ReservationResult(true, "Cập nhật đặt bàn thành công.", _mapper.Map<ReservationDTO>(reservation));
        }

        public async Task<ReservationResult> GetByWeekAsync(DateTime weekStart)
        {
            // Đảm bảo weekStart là đầu ngày thứ Hai
            var startOfWeek = weekStart.Date;
            var endOfWeek = startOfWeek.AddDays(7);

            var reservations = await _context.Reservations
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Table)
                .Where(r => r.ReservationTime >= startOfWeek && r.ReservationTime < endOfWeek)
                .OrderBy(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult(true, "Lấy lịch đặt bàn theo tuần thành công.",
                reservations: _mapper.Map<List<ReservationDTO>>(reservations));
        }

        public async Task<ReservationResult> CancelAsync(int reservationId, Guid customerPublicId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
                return new ReservationResult(false, "Đặt bàn không tồn tại.");

            if (reservation.Customer.PublicId != customerPublicId)
                return new ReservationResult(false, "Bạn không có quyền huỷ đặt bàn này.");

            if (reservation.Status != "Pending")
                return new ReservationResult(false, $"Không thể huỷ đặt bàn ở trạng thái '{reservation.Status}'.");

            reservation.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return new ReservationResult(true, "Huỷ đặt bàn thành công.");
        }
    }
}
