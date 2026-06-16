using IdentityEmail.Context;
using IdentityEmail.Entities;
using IdentityEmail.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.ViewComponents.MessageLayout
{
    public class MessageSidebar : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageSidebar(
            EmailContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new MessageSidebarViewModel
            {
                InboxCount = await _context.UserMessages.CountAsync(x =>
                    x.ReceiverId == user.Id &&
                    x.Folder == MessageFolder.Inbox),

                StarredCount = await _context.UserMessages.CountAsync(x =>
                    x.ReceiverId == user.Id &&
                    x.IsStarred),

                DraftCount = await _context.UserMessages.CountAsync(x =>
                    x.SenderId == user.Id &&
                    x.Folder == MessageFolder.Draft),

                SentCount = await _context.UserMessages.CountAsync(x =>
                    x.SenderId == user.Id &&
                    x.Folder == MessageFolder.Sent),

                ScheduleCount = await _context.UserMessages.CountAsync(x =>
                    x.SenderId == user.Id &&
                    x.Folder == MessageFolder.Schedule),

                ArchiveCount = await _context.UserMessages.CountAsync(x =>
                    x.ReceiverId == user.Id &&
                    x.Folder == MessageFolder.Archive),

                SpamCount = await _context.UserMessages.CountAsync(x =>
                    x.ReceiverId == user.Id &&
                    x.Folder == MessageFolder.Spam),

                TrashCount = await _context.UserMessages.CountAsync(x =>
                    x.ReceiverId == user.Id &&
                    x.Folder == MessageFolder.Trash),

                Categories = await _context.MessageCategories
                    .Include(x => x.Messages)
                    .Where(x => x.UserId == user.Id)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}