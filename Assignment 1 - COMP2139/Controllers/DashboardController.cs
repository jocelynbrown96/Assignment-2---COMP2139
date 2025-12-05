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
            // Get logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Load all purchases for this user — including events + categories
            var purchases = await _context.Purchases
                .Where(p => p.UserId == user.Id)
                .Include(p => p.PurchaseEvents)
                .ThenInclude(pe => pe.Event)
                .ThenInclude(e => e.Category)
                .ToListAsync();

            return View(purchases);
        }
    }
}