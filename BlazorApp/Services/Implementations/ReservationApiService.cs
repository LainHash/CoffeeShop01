using BlazorApp.Models.Reservations;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class ReservationApiService : IReservationApiService
    {
        private readonly IApiService _apiService;

        public ReservationApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ReservationListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<ReservationListResponse>("/api/Reservation");
        }

        public async Task<ReservationResponse?> GetByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<ReservationResponse>($"/api/Reservation/{id}");
        }

        public async Task<ReservationResponse?> CreateAsync(CreateReservationInput input)
        {
            return await _apiService.PostAsync<CreateReservationInput, ReservationResponse>("/api/Reservation", input);
        }

        public async Task<ReservationResponse?> UpdateAsync(Guid id, UpdateReservationInput input)
        {
            return await _apiService.PutAsync<UpdateReservationInput, ReservationResponse>($"/api/Reservation/{id}", input);
        }
    }
}
