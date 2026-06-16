using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.ViewComponents.DashboardLayout
{
    public class DashboardData : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public DashboardData(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var hourlyDistribution = _context.UserMessages
     .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
     .AsEnumerable() // SentAt.Hour gibi client-side fonksiyonlar için
     .GroupBy(m => m.SentAt.Hour)
     .Select(g => new { Hour = g.Key, Count = g.Count() })
     .OrderByDescending(g => g.Count)
     .ToList();

            var peakHour = hourlyDistribution.FirstOrDefault();
            var peak = peakHour?.Hour ?? 0;
            
            var totalUser= await _userManager.Users.CountAsync();
            var totalDepartman = await _userManager.Users.Where(x => !string.IsNullOrEmpty(x.JobTitle)).Select(x=>x.JobTitle).Distinct().CountAsync();

            ViewBag.TotalUser = totalUser;
            ViewBag.TotalDepartman = totalDepartman;



            return View(peak);
          

        }
    } 
}
