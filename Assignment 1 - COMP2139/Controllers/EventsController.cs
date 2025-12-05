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
        // CREATE (Only Admin + Organizer)
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

            // Assign organizer ID (Admin events have null organizer)
            var user = await _userManager.GetUserAsync(User);
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
        // EDIT (Admin can edit any event; Organizer only their own)
        // -----------------------------
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            // Organizer can only edit their own events
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

            // Check ownership before saving
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

            try
            {
                updatedEvent.Date = DateTime.SpecifyKind(updatedEvent.Date, DateTimeKind.Utc);
                _context.Update(updatedEvent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.Id == updatedEvent.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // DELETE (Admin can delete anything; Organizer only their own)
        // -----------------------------
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            // Organizer must own the event
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

            // Ownership enforcement
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