using Microsoft.AspNetCore.Mvc;

namespace RazorPage.ViewComponents
{
    public class PageHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title)
        {
            return View("Default", title);
        }
    }
}
