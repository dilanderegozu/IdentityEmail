using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.ViewComponents.DashboardLayout
{
    public class DashboardStatic : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public DashboardStatic(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            var user = await _userManager.GetUserAsync(HttpContext.User);

          
            var recentMessages = await _context.UserMessages
    .Include(m => m.Sender)
    .Where(m => m.ReceiverId == user.Id &&
                    m.Folder == MessageFolder.Inbox)
    .OrderByDescending(m => m.SentAt)
    .Take(3)
    .ToListAsync();
            return View(recentMessages);
        
    }
    }
}