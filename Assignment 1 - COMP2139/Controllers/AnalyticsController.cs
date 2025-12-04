using Assignment_1___COMP2139.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1___COMP2139.Controllers
{
    [Authorize(Roles = "Organizer,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------
        // 1. Ticket sales by category
        // ---------------------------
        [HttpGet("TicketSalesByCategory")]
        public async Task<IActionResult> TicketSalesByCategory()
        {
            var data = await _context.Categories
                .Select(c => new
                {
                    Category = c.Name,
                    TicketsSold = c.Events
                        .SelectMany(e => e.PurchaseEvents)
                        .Sum(pe => (int?)pe.Quantity) ?? 0
                })
                .ToListAsync();

            return Ok(data);
        }

        // ---------------------------
        // 2. Revenue per month
        // ---------------------------
        [HttpGet("RevenuePerMonth")]
        public async Task<IActionResult> RevenuePerMonth()
        {
            var data = await _context.Purchases
                .SelectMany(p => p.PurchaseEvents)
                .Include(pe => pe.Event)
                .GroupBy(pe => new { pe.Purchase.PurchaseDate.Year, pe.Purchase.PurchaseDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(pe => pe.Quantity * pe.Event.TicketPrice)
                })
                .OrderBy(d => d.Year).ThenBy(d => d.Month)
                .ToListAsync();

            return Ok(data);
        }

        // ---------------------------
        // 3. Top 5 best-selling events
        // ---------------------------
        [HttpGet("TopEvents")]
        public async Task<IActionResult> TopEvents()
        {
            var data = await _context.Events
                .Select(e => new
                {
                    e.Title,
                    TicketsSold = e.PurchaseEvents.Sum(pe => (int?)pe.Quantity) ?? 0,
                    Revenue = e.PurchaseEvents.Sum(pe => (int?)pe.Quantity * e.TicketPrice) ?? 0
                })
                .OrderByDescending(e => e.TicketsSold)
                .Take(5)
                .ToListAsync();

            return Ok(data);
        }
    }
}