using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Assignment_1___COMP2139.Controllers
{
    [Authorize] // Require login to purchase
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchasesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Purchases/Create?eventId=#
        public async Task<IActionResult> Create(int? eventId)
        {
            if (eventId == null)
                return NotFound();

            var ev = await _context.Events.FindAsync(eventId.Value);
            if (ev == null)
                return NotFound();

            var model = new PurchaseViewModel
            {
                EventId = ev.Id,
                EventTitle = ev.Title,
                EventDate = ev.Date,
                TicketPrice = ev.TicketPrice,
                AvailableTickets = ev.AvailableTickets,
                Quantity = 1
            };

            return View(model);
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ev = await _context.Events.FindAsync(model.EventId);
            if (ev == null)
            {
                ModelState.AddModelError("", "Event not found.");
                return View(model);
            }

            if (model.Quantity < 1 || model.Quantity > ev.AvailableTickets)
            {
                ModelState.AddModelError("", $"Please enter a quantity between 1 and {ev.AvailableTickets}.");
                return View(model);
            }

            //  Get logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "You must be logged in to complete this purchase.");
                return View(model);
            }

            //  Create purchase with user ID
            var purchase = new Purchase
            {
                GuestName = model.GuestName,
                GuestEmail = model.GuestEmail,
                PurchaseDate = DateTime.UtcNow,
                UserId = user.Id, // << MUST HAVE THIS
                PurchaseEvents = new List<PurchaseEvent>
                {
                    new PurchaseEvent
                    {
                        EventId = ev.Id,
                        Quantity = model.Quantity
                    }
                }
            };

            // Reduce available tickets
            ev.AvailableTickets -= model.Quantity;

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = purchase.Id });
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseEvents)
                    .ThenInclude(pe => pe.Event)
                        .ThenInclude(e => e.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var purchases = await _context.Purchases
                .Include(p => p.PurchaseEvents)
                    .ThenInclude(pe => pe.Event)
                        .ThenInclude(e => e.Category)
                .ToListAsync();

            return View(purchases);
        }
    }
}