using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.DashboardLayout
{
    public class DashboardHead:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
