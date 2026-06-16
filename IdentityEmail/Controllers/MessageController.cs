using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public MessageController(UserManager<AppUser> userManager, EmailContext context)
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

            var unreadMessages = await _context.UserMessages.Include(m => m.Sender)
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

            ViewBag.UnreadMessages = totalUnreadCount;
            ViewBag.UnreadMessagesList = unreadMessages;
            return View();
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }

        private async Task SetCategoriesViewBagAsync(string userId)
        {
            ViewBag.Categories = await _context.MessageCategories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IActionResult> Inbox(bool? isRead, bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Category)
                .Where(m => m.ReceiverId == user.Id && m.Folder == MessageFolder.Inbox);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Gelen Kutusu";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Sent(bool? isRead, bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Receiver)
                .Include(m => m.Category)
                .Where(m => m.SenderId == user.Id && m.Folder == MessageFolder.Sent);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Gönderilmiş";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Draft(bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Category)
                .Where(m => m.SenderId == user.Id && m.Folder == MessageFolder.Draft);

            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Taslaklar";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Schedule(bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Category)
                .Where(m => m.SenderId == user.Id && m.Folder == MessageFolder.Schedule);

            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Planlanmış";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Archive(bool? isRead, bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Category)
                .Where(m => (m.SenderId == user.Id || m.ReceiverId == user.Id) && m.Folder == MessageFolder.Archive);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Arşiv";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Spam(bool? isRead, bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Category)
                .Where(m => m.ReceiverId == user.Id && m.Folder == MessageFolder.Spam);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Spam";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Trash(bool? isRead, bool? starred, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Category)
                .Where(m => (m.SenderId == user.Id || m.ReceiverId == user.Id) && m.Folder == MessageFolder.Trash);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Çöp Kutusu";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Starred(bool? isRead, int? categoryId)
        {
            var user = await GetCurrentUserAsync();
            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Category)
                .Where(m => (m.SenderId == user.Id || m.ReceiverId == user.Id) && m.IsStarred);

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = "Yıldızlı";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        public async Task<IActionResult> Category(int id, bool? isRead, bool? starred)
        {
            var user = await GetCurrentUserAsync();
            var category = await _context.MessageCategories.FindAsync(id);

            var query = _context.UserMessages
                .Include(m => m.Sender)
                .Where(m => m.CategoryId == id && (m.SenderId == user.Id || m.ReceiverId == user.Id));

            if (isRead.HasValue) query = query.Where(m => m.IsRead == isRead.Value);
            if (starred.HasValue) query = query.Where(m => m.IsStarred == starred.Value);

            var messages = await query.OrderByDescending(m => m.SentAt).ToListAsync();

            ViewBag.FolderTitle = category?.Name ?? "Etiket";
            await SetCategoriesViewBagAsync(user.Id);
            return View("Folder", messages);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStar(int id)
        {
            var message = await _context.UserMessages.FindAsync(id);
            if (message == null) return NotFound();

            message.IsStarred = !message.IsStarred;
            await _context.SaveChangesAsync();

            return Json(new { success = true, isStarred = message.IsStarred });
        }

        [HttpPost]
        public async Task<IActionResult> MoveToFolder(int id, MessageFolder folder)
        {
            var message = await _context.UserMessages.FindAsync(id);
            if (message == null) return NotFound();

            message.Folder = folder;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveMessage(int id)
        {
            return await MoveToFolder(id, MessageFolder.Archive);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            return await MoveToFolder(id, MessageFolder.Trash);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var message = await _context.UserMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message == null) return NotFound();

            var user = await GetCurrentUserAsync();
            if (message.ReceiverId == user.Id && !message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> ToggleRead(int id)
        {
            var message = await _context.UserMessages.FindAsync(id);
            if (message == null) return NotFound();

            message.IsRead = !message.IsRead;
            await _context.SaveChangesAsync();

            return Json(new { success = true, isRead = message.IsRead });
        }
    }
}