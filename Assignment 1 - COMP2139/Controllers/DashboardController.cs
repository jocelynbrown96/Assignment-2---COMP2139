using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1___COMP2139.Controllers
{
    [Authorize] // Only logged-in users can see the dashboard
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // 1) Load this user's purchases (for My Tickets / History)
            var purchases = await _context.Purchases
                .Where(p => p.UserId == user.Id)
                .Include(p => p.PurchaseEvents)
                .ThenInclude(pe => pe.Event)
                .ThenInclude(e => e.Category)
                .ToListAsync();

            // 2) If they are an Organizer, load events they created
            List<Event> myEvents = new();
            if (User.IsInRole("Organizer"))
            {
                myEvents = await _context.Events
                    .Where(e => e.OrganizerId == user.Id)
                    .Include(e => e.Category)
                    .ToListAsync();
            }

            // 3) Pass organizer events to the view
            ViewBag.MyEvents = myEvents;

            // View still strongly-typed to purchases
            return View(purchases);
        }
    }
}