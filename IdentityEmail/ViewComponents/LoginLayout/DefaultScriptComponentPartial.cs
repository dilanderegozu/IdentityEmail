using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.LoginLayout
{
    public class DefaultScriptComponentPartial:ViewComponent
        
    {
       public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
