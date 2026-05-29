using WebAPI.DTOs.Reservations;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResult<ReservationDTO>> CreateAsync(CreateReservationDTO dto);

        Task<ReservationResult<List<ReservationDTO>>> GetByCustomerAsync(Guid customerPublicId);

        Task<ReservationResult<List<ReservationDTO>>> GetAllAsync();

        Task<ReservationResult<ReservationDTO>> GetByIdAsync(Guid id);

        Task<ReservationResult<ReservationDTO>> UpdateAsync(Guid id, UpdateReservationDTO dto);

        Task<ReservationResult<List<ReservationDTO>>> GetByWeekAsync(DateTime weekStart);
    }
}
