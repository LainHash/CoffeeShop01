namespace BlazorApp.Helpers.Constants
{
    public static class OrderStatuses
    {
        public const string Unpaid = "Unpaid";
        public const string Paid = "Paid";
        public const string Cancelled = "Cancelled";

        public static string ViTranslate(string status)
        {
            var viDict = new Dictionary<string, string>()
            {
                {Paid, "Đã thanh toán" },
                {Unpaid, "Chưa thanh toán" },
                {Cancelled, "Đã hủy" }
            };
            return viDict[status];
        }
    }
}
