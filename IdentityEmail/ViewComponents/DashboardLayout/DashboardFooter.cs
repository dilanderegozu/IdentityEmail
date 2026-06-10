using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.DashboardLayout
{
    public class DashboardFooter:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
