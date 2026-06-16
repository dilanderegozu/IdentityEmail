using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.ViewComponents.MessageLayout
{
    public class MessageNavbar : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public MessageNavbar(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(UserClaimsPrincipal.Identity.Name);

            var unreadMessages = await _context.UserMessages
                .Include(m => m.Sender)
                .Where(m => m.ReceiverId == user.Id && !m.IsRead && m.Folder == MessageFolder.Inbox)
                .OrderByDescending(m => m.SentAt)
                .Take(3)
                .ToListAsync();

            ViewBag.UnreadMessages = unreadMessages.Count;
            ViewBag.UnreadMessagesList = unreadMessages;
            ViewBag.User = user;

            return View();
        }
    }
}