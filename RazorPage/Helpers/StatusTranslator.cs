using RazorPage.Helpers.Constants.Orders;

namespace RazorPage.Helpers
{
    public static class StatusTranslator
    {
        public static string TranslateTableStatus(string status)
        {
            return status switch
            {
                TableStatuses.Available => "Trống",
                TableStatuses.Occupied => "Đang sử dụng",
                TableStatuses.Reserved => "Đã đặt",
                TableStatuses.Unavailable => "Không khả dụng",
                _ => status
            };
        }

        public static string TranslateReservationStatus(string status)
        {
            return status switch
            {
                ReservationStatuses.Pending => "Chờ xác nhận",
                ReservationStatuses.Confirmed => "Đã xác nhận",
                ReservationStatuses.Completed => "Hoàn thành",
                ReservationStatuses.Cancelled => "Đã hủy",
                _ => status
            };
        }

        public static string TranslateInvoiceStatus(string status)
        {
            return status switch
            {
                InvoiceStatuses.Unpaid => "Chưa thanh toán",
                InvoiceStatuses.Paid => "Đã thanh toán",
                InvoiceStatuses.Cancelled => "Đã hủy",
                _ => status
            };
        }
    }
}
