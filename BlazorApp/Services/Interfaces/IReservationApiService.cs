using BlazorApp.Models.Reservations;

namespace BlazorApp.Services.Interfaces
{
    public interface IReservationApiService
    {
        Task<ReservationListResponse?> GetAllAsync();
        Task<ReservationResponse?> GetByIdAsync(int id);
        Task<ReservationResponse?> CreateAsync(CreateReservationInput input);
        Task<ReservationResponse?> UpdateAsync(int id, UpdateReservationInput input);
    }
}
