using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1___COMP2139.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // -----------------------------
        // INDEX (Everyone can view)
        // -----------------------------
        public async Task<IActionResult> Index(string searchString, int? categoryId, string sortOrder)
        {
            // ----------------------------------------------------
            // TEMP PROMOTION (remove this after testing)
            // ----------------------------------------------------
            var promoteUser = await _userManager.FindByEmailAsync("kelly@gmail.com");
            if (promoteUser != null)
            {
                await _userManager.AddToRoleAsync(promoteUser, "Admin");
            }
            // ----------------------------------------------------

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId;

            var events = _context.Events.Include(e => e.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e =>
                    e.Title.ToLower().Contains(searchString.ToLower()) ||
                    e.Location.ToLower().Contains(searchString.ToLower()));
            }

            if (categoryId.HasValue)
            {
                events = events.Where(e => e.CategoryId == categoryId.Value);
            }

            switch (sortOrder)
            {
                case "priceAsc":
                    events = events.OrderBy(e => e.TicketPrice);
                    break;

                case "priceDesc":
                    events = events.OrderByDescending(e => e.TicketPrice);
                    break;
            }

            return View(await events.ToListAsync());
        }

        // -----------------------------
        // DETAILS (Everyone can view)
        // -----------------------------
        public async Task<IActionResult> Details(int id)
        {
            var eventItem = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            return View(eventItem);
        }

        // -----------------------------
        // CREATE (Admin + Organizer)
        // -----------------------------
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(newEvent);
            }

            var user = await _userManager.GetUserAsync(User);

            // Only organizer events get an organizer ID
            if (User.IsInRole("Organizer"))
            {
                newEvent.OrganizerId = user.Id;
            }

            newEvent.Date = DateTime.SpecifyKind(newEvent.Date, DateTimeKind.Utc);

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // EDIT (Admin = any; Organizer = only theirs)
        // -----------------------------
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            if (User.IsInRole("Organizer"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (eventItem.OrganizerId != user.Id)
                    return Forbid();
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(eventItem);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event updatedEvent)
        {
            if (id != updatedEvent.Id)
                return NotFound();

            // Check organizer ownership
            if (User.IsInRole("Organizer"))
            {
                var user = await _userManager.GetUserAsync(User);
                var originalEvent = await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

                if (originalEvent == null)
                    return NotFound();

                if (originalEvent.OrganizerId != user.Id)
                    return Forbid();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(updatedEvent);
            }

            updatedEvent.Date = DateTime.SpecifyKind(updatedEvent.Date, DateTimeKind.Utc);

            _context.Update(updatedEvent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // DELETE (Admin = any; Organizer = only theirs)
        // -----------------------------
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            if (User.IsInRole("Organizer"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (eventItem.OrganizerId != user.Id)
                    return Forbid();
            }

            return View(eventItem);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
                return RedirectToAction(nameof(Index));

            if (User.IsInRole("Organizer"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (eventItem.OrganizerId != user.Id)
                    return Forbid();
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}