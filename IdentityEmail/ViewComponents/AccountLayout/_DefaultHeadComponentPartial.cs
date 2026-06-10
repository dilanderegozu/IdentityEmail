using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.LoginLayout
{
    public class _DefaultHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
