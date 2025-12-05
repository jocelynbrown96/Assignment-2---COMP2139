using System.ComponentModel.DataAnnotations;

namespace Assignment_1___COMP2139.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The event title is required.")]
        [Display(Name = "Event Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a date for the event.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please set a ticket price.")]
        [Range(0.01, 10000, ErrorMessage = "Ticket price must be greater than zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available tickets cannot be negative.")]
        [Display(Name = "Available Tickets")]
        public int AvailableTickets { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<PurchaseEvent> PurchaseEvents { get; set; } = new List<PurchaseEvent>();

        // -----------------------------
        // NEW — Organizer Ownership
        // -----------------------------

        // The user who created the event (only Organizers)
        public string? OrganizerId { get; set; }

        // Navigation to the user account
        public ApplicationUser? Organizer { get; set; }
    }
}