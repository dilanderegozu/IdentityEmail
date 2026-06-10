using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public DashboardController(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users
                .Include(u => u.SendMessages)
                .Include(u => u.ReceivedMessages)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null)
                return RedirectToAction("Login", "Account");
            var unreadMessages = await _context.UserMessages
            .Include(m => m.Sender)
            .Where(m => m.ReceiverId == user.Id && !m.IsRead && m.Folder == MessageFolder.Inbox)
            .OrderByDescending(m => m.SentAt)
            .Take(3)
            .ToListAsync();

            var last7Days = Enumerable.Range(0, 7).Select(x=>DateTime.Today.AddDays(-6+x)).ToList();
            var dailyCounts=last7Days.Select(x=>_userManager.Users.Count(y=>y.RegisteredAt.Date == x)).ToList();
            var maxCount = dailyCounts.Max() == 0 ? 1 : dailyCounts.Max();
            var heights = dailyCounts.Select(c => (int)((c / (double)maxCount) * 100)).ToList();
            var thisWeek = _userManager.Users.Count(u => u.RegisteredAt >= DateTime.Today.AddDays(-7));
            var lastWeek = _userManager.Users.Count(u => u.RegisteredAt >= DateTime.Today.AddDays(-14)
                                                       && u.RegisteredAt < DateTime.Today.AddDays(-7));
            double changePercent = lastWeek == 0 ? 100 : ((thisWeek - lastWeek) / (double)lastWeek) * 100;
   
            var dailyUnreadCounts=last7Days.Select(x=>_context.UserMessages.Count(m=>m.ReceiverId == user.Id && !m.IsRead && m.SentAt.Date == x.Date)).ToList();

            var maxUnread = dailyUnreadCounts.Max() == 0 ? 1 : dailyUnreadCounts.Max();
            var unreadHeights = dailyUnreadCounts
                .Select(c => (int)((c / (double)maxUnread) * 100))
                .ToList();

            // Geçen haftaya göre değişim
            var thisWeekUnread = _context.UserMessages.Count(m =>
                m.ReceiverId == user.Id && !m.IsRead &&
                m.SentAt >= DateTime.Today.AddDays(-7));
            var lastWeekUnread = _context.UserMessages.Count(m =>
                m.ReceiverId == user.Id && !m.IsRead &&
                m.SentAt >= DateTime.Today.AddDays(-14) &&
                m.SentAt < DateTime.Today.AddDays(-7));

            double unreadChangePercent = lastWeekUnread == 0 ? 100 :
                ((thisWeekUnread - lastWeekUnread) / (double)lastWeekUnread) * 100;


            ViewBag.UnreadHeights = unreadHeights;
            ViewBag.UnreadChangePercent = Math.Round(unreadChangePercent, 1);
            ViewBag.UserChangePercent = Math.Round(changePercent, 1);
            ViewBag.DailyHeights = heights;
            ViewBag.UnreadMessagesList = unreadMessages;
            ViewBag.UnreadMessages = unreadMessages.Count;
            ViewBag.SendMessages = user.SendMessages?.Count() ?? 0;
            ViewBag.ReceivedMessages = user.ReceivedMessages?.Count ?? 0;
            ViewBag.TotalUsers = _userManager.Users.Count();
            return View();
        }
    }
}