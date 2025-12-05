using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using QRCoder;

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

        // ============================
        // GET: Purchases/Create?eventId=#
        // ============================
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

        // ============================
        // POST: Purchases/Create
        // ============================
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

            // Logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "You must be logged in to complete this purchase.");
                return View(model);
            }

            // Create purchase
            var purchase = new Purchase
            {
                GuestName = model.GuestName,
                GuestEmail = model.GuestEmail,
                PurchaseDate = DateTime.UtcNow,
                UserId = user.Id,
                PurchaseEvents = new List<PurchaseEvent>
                {
                    new PurchaseEvent
                    {
                        EventId = ev.Id,
                        Quantity = model.Quantity
                    }
                }
            };

            // Decrease tickets
            ev.AvailableTickets -= model.Quantity;

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = purchase.Id });
        }

        // ============================
        // GET: Purchases/Details/5
        // ============================
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

        // ============================
        // DOWNLOAD TICKET (TXT FILE)
        // ============================
        [HttpGet]
        public async Task<IActionResult> DownloadTicket(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseEvents)
                    .ThenInclude(pe => pe.Event)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
                return NotFound();

            // Build human-readable ticket file
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("===== TICKET CONFIRMATION =====");
            sb.AppendLine($"Name: {purchase.GuestName}");
            sb.AppendLine($"Email: {purchase.GuestEmail}");
            sb.AppendLine($"Purchase Date: {purchase.PurchaseDate:MMMM dd, yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine("Tickets:");

            foreach (var pe in purchase.PurchaseEvents)
            {
                sb.AppendLine($"Event: {pe.Event.Title}");
                sb.AppendLine($"Date: {pe.Event.Date:MMMM dd, yyyy}");
                sb.AppendLine($"Quantity: {pe.Quantity}");
                sb.AppendLine($"Total: ${(pe.Quantity * pe.Event.TicketPrice):F2}");
                sb.AppendLine("----------------------------");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            var fileName = $"Ticket_{purchase.Id}.txt";

            return File(bytes, "text/plain", fileName);
        }

        // ============================
// GET: QR Code for Ticket
// ============================
        [HttpGet]
        public IActionResult GetQrCode(int id)
        {
            // Create QR content (what scanning the code does)
            string qrText = $"TicketMe - Ticket #{id}";

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrData);
            var qrBytes = qrCode.GetGraphic(8);

            return File(qrBytes, "image/png");
        }


        // ============================
        // LIST ALL PURCHASES (USER)
        // ============================
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

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