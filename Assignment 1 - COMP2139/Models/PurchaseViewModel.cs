using System.ComponentModel.DataAnnotations;

namespace Assignment_1___COMP2139.Models
{
    public class PurchaseViewModel
    {
        public int EventId { get; set; }

        [Display(Name = "Event")]
        public string EventTitle { get; set; } = string.Empty;

        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Display(Name = "Available Tickets")]
        public int AvailableTickets { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string GuestName { get; set; } = string.Empty;

        [Required, EmailAddress]
        [Display(Name = "Email Address")]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter at least 1 ticket")]
        public int Quantity { get; set; }
    }
}