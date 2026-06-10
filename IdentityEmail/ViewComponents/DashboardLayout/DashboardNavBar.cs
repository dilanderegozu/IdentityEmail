using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.DashboardLayout
{
    public class DashboardNavBar:ViewComponent
        {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
