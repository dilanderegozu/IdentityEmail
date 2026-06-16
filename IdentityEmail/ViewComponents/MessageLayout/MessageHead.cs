using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.MessageLayout
{
    public class MessageHead: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
