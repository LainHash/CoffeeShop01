using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Order;
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

        // ────────────────────────────────────────────────────
        // Properties for the view
        // ────────────────────────────────────────────────────

        /// <summary>Ngày đầu tuần (Thứ Hai) đang xem</summary>
        public DateTime WeekStart { get; private set; }

        /// <summary>Ngày đầu tuần hiện tại (để nút "Tuần này")</summary>
        public DateTime CurrentWeekStart { get; private set; }

        /// <summary>Lịch đặt bàn nhóm theo ngày</summary>
        public Dictionary<DateTime, List<ReservationApiItem>> ReservationsByDay { get; private set; } = new();

        /// <summary>Tổng số lịch đặt trong tuần</summary>
        public int TotalReservations { get; private set; }

        public string? ErrorMessage { get; private set; }

        // ────────────────────────────────────────────────────
        // Helpers
        // ────────────────────────────────────────────────────

        /// <summary>Tính ngày Thứ Hai của tuần chứa <paramref name="date"/>.</summary>
        private static DateTime GetMondayOf(DateTime date)
        {
            int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            return date.Date.AddDays(-diff);
        }

        // ────────────────────────────────────────────────────
        // Handlers
        // ────────────────────────────────────────────────────

        public async Task<IActionResult> OnGetAsync([FromQuery] string? weekStart)
        {
            // Tính ngày Thứ Hai của tuần hiện tại
            CurrentWeekStart = GetMondayOf(DateTime.Today);

            // Parse weekStart từ query hoặc dùng tuần hiện tại
            if (DateTime.TryParse(weekStart, out var parsedWeek))
                WeekStart = GetMondayOf(parsedWeek);
            else
                WeekStart = CurrentWeekStart;

            // Khởi tạo dict 7 ngày rỗng
            for (int i = 0; i < 7; i++)
                ReservationsByDay[WeekStart.AddDays(i).Date] = new List<ReservationApiItem>();

            // Gọi API
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
