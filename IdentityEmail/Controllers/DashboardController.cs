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
                .Where(m =>
                    m.ReceiverId == user.Id &&
                    !m.IsRead &&
                    m.Folder == MessageFolder.Inbox)
                .OrderByDescending(m => m.SentAt)
                .Take(3)
                .ToListAsync();

       
            var totalUnreadCount = await _context.UserMessages
                .CountAsync(m =>
                    m.ReceiverId == user.Id &&
                    !m.IsRead &&
                    m.Folder == MessageFolder.Inbox);


   
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(-6 + i))
                .ToList();

       
            var dailyCounts = last7Days
                .Select(day =>
                {
                    var nextDay = day.AddDays(1);

                    return _userManager.Users.Count(u =>
                        u.RegisteredAt >= day &&
                        u.RegisteredAt < nextDay);
                })
                .ToList();

            var maxCount = dailyCounts.Max() == 0 ? 1 : dailyCounts.Max();
            var heights = dailyCounts
                .Select(c => (int)((c / (double)maxCount) * 100))
                .ToList();

            var thisWeek = _userManager.Users.Count(u =>
                u.RegisteredAt >= DateTime.Today.AddDays(-7));

            var lastWeek = _userManager.Users.Count(u =>
                u.RegisteredAt >= DateTime.Today.AddDays(-14) &&
                u.RegisteredAt < DateTime.Today.AddDays(-7));

            double changePercent = lastWeek == 0
                ? 100
                : ((thisWeek - lastWeek) / (double)lastWeek) * 100;

        
            var dailyUnreadCounts = last7Days
                .Select(day =>
                {
                    var nextDay = day.AddDays(1);

                    return _context.UserMessages.Count(m =>
                        m.ReceiverId == user.Id &&
                        !m.IsRead &&
                        m.SentAt >= day &&
                        m.SentAt < nextDay);
                })
                .ToList();

            var maxUnread = dailyUnreadCounts.Max() == 0
                ? 1
                : dailyUnreadCounts.Max();

            var unreadHeights = dailyUnreadCounts
                .Select(c => (int)((c / (double)maxUnread) * 100))
                .ToList();

            var thisWeekUnread = _context.UserMessages.Count(m =>
                m.ReceiverId == user.Id &&
                !m.IsRead &&
                m.SentAt >= DateTime.Today.AddDays(-7));

            var lastWeekUnread = _context.UserMessages.Count(m =>
                m.ReceiverId == user.Id &&
                !m.IsRead &&
                m.SentAt >= DateTime.Today.AddDays(-14) &&
                m.SentAt < DateTime.Today.AddDays(-7));

            double unreadChangePercent = lastWeekUnread == 0
                ? 100
                : ((thisWeekUnread - lastWeekUnread) / (double)lastWeekUnread) * 100;

       
            var weeklyMessageCounts = last7Days
                .Select(day =>
                {
                    var nextDay = day.AddDays(1);

                    return _context.UserMessages.Count(m =>
                        (m.SenderId == user.Id || m.ReceiverId == user.Id) &&
                        m.SentAt >= day &&
                        m.SentAt < nextDay);
                })
                .ToList();

       
            var monthlyMessageCounts = Enumerable.Range(0, 12)
                .Select(i =>
                {
                    var monthStart = new DateTime(
                        DateTime.Today.AddMonths(-11 + i).Year,
                        DateTime.Today.AddMonths(-11 + i).Month,
                        1);

                    var nextMonth = monthStart.AddMonths(1);

                    return _context.UserMessages.Count(m =>
                        (m.SenderId == user.Id || m.ReceiverId == user.Id) &&
                        m.SentAt >= monthStart &&
                        m.SentAt < nextMonth);
                })
                .ToList();

            var monthlyLabels = Enumerable.Range(0, 12)
                .Select(i => DateTime.Today
                    .AddMonths(-11 + i)
                    .ToString("MMM"))
                .ToList();
            var dailySentCounts = last7Days.Select(day =>
            _context.UserMessages.Count(m => m.SentAt.Date == day.Date)).ToList();

            var maxSent = dailySentCounts.Max() == 0 ? 1 : dailySentCounts.Max();
            var sentHeights = dailySentCounts.Select(c => (int)((c / (double)maxSent) * 100)).ToList();

            var thisWeekSent = _context.UserMessages.Count(m => m.SentAt >= DateTime.Today.AddDays(-7));
            var lastWeekSent = _context.UserMessages.Count(m =>
                m.SentAt >= DateTime.Today.AddDays(-14) && m.SentAt < DateTime.Today.AddDays(-7));

            double sentChangePercent = lastWeekSent == 0 ? 100 :
                ((thisWeekSent - lastWeekSent) / (double)lastWeekSent) * 100;

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var todaySentCount = await _context.UserMessages
             .CountAsync(m =>
             m.SenderId == user.Id &&
             m.SentAt >= today &&
             m.SentAt < tomorrow);


            ViewBag.SentHeights = sentHeights;
            ViewBag.SentCounts = dailySentCounts;
            ViewBag.SentChangePercent = Math.Round(sentChangePercent, 1);
            ViewBag.TodaySent = dailySentCounts.Last();
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.UserChangePercent = Math.Round(changePercent, 1);
            ViewBag.DailyCounts = dailyCounts;
            ViewBag.DailyHeights = heights;
            ViewBag.UnreadMessages = totalUnreadCount;
            ViewBag.UnreadMessagesList = unreadMessages;
            ViewBag.UnreadCounts = dailyUnreadCounts;
            ViewBag.UnreadHeights = unreadHeights;
            ViewBag.UnreadChangePercent = Math.Round(unreadChangePercent, 1);
            ViewBag.SendMessages = user.SendMessages?.Count ?? 0;
            ViewBag.ReceivedMessages = user.ReceivedMessages?.Count ?? 0;
            ViewBag.WeeklyMessageCounts = weeklyMessageCounts;
            ViewBag.MonthlyMessageCounts = monthlyMessageCounts;
            ViewBag.MonthlyLabels = monthlyLabels;
            ViewBag.TodaySent = todaySentCount;
            return View();
        }
    }
}