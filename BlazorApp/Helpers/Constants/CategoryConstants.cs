namespace BlazorApp.Helpers.Constants
{
    public class CategoryConstants
    {
        public const string Coffee = "Coffees";
        public const string Snack = "Snacks";
        public const string Cake = "Cakes";
        public const string SoftDrink = "Soft Drinks";

        public static string ViTrans(string categoryName)
        {
            var viDict = new Dictionary<string, string>()
            {
                {Coffee, "Cà phê" },
                {Snack, "Đồ ăn vặt" },
                {Cake, "Bánh ngọt" },
                {SoftDrink, "Nước giải khát" }
            };
            return viDict[categoryName];
        }
    }
}
