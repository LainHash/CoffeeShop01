using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Orders.Reservations;
using System.Text.Json;

namespace RazorPage.Pages.Order
{
    public class ReservationScheduleModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ReservationScheduleModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DateTime WeekStart { get; private set; }
        public DateTime CurrentWeekStart { get; private set; }
        public Dictionary<DateTime, List<ReservationApiItem>> ReservationsByDay { get; private set; } = new();
        public int TotalReservations { get; private set; }

        public string? ErrorMessage { get; private set; }
        private static DateTime GetMondayOf(DateTime date)
        {
            int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            return date.Date.AddDays(-diff);
        }
        public async Task<IActionResult> OnGetAsync([FromQuery] string? weekStart)
        {
            CurrentWeekStart = GetMondayOf(DateTime.Today);

            if (DateTime.TryParse(weekStart, out var parsedWeek))
                WeekStart = GetMondayOf(parsedWeek);
            else
                WeekStart = CurrentWeekStart;

            for (int i = 0; i < 7; i++)
                ReservationsByDay[WeekStart.AddDays(i).Date] = new List<ReservationApiItem>();

            try
            {
                var client = _httpClientFactory.CreateClient("WebAPI");
                var weekStartStr = WeekStart.ToString("yyyy-MM-ddTHH:mm:ss");
                var response = await client.GetAsync($"/api/Reservation/Week?weekStart={Uri.EscapeDataString(weekStartStr)}");

                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = $"Lỗi khi tải dữ liệu (HTTP {(int)response.StatusCode}).";
                    return Page();
                }

                var body = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ReservationListApiResponse>(body, _jsonOptions);

                if (result?.Success == true && result.Reservations != null)
                {
                    foreach (var r in result.Reservations)
                    {
                        var day = r.ReservationTime.Date;
                        if (ReservationsByDay.ContainsKey(day))
                            ReservationsByDay[day].Add(r);
                    }

                    TotalReservations = result.Reservations.Count;
                }
                else
                {
                    ErrorMessage = result?.Message ?? "Không thể tải dữ liệu lịch đặt bàn.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Không thể kết nối đến máy chủ: {ex.Message}";
            }

            return Page();
        }
    }
}
