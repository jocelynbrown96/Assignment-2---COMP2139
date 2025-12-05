using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_1___COMP2139.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required, EmailAddress]
        public string GuestEmail { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        // ⭐ NEW — Link purchase to logged-in user
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        // Many-to-many navigation
        public ICollection<PurchaseEvent> PurchaseEvents { get; set; } = new List<PurchaseEvent>();

        // Total calculated cost
        [NotMapped]
        public decimal TotalCost =>
            PurchaseEvents?.Sum(pe => (pe.Event?.TicketPrice ?? 0) * pe.Quantity) ?? 0;
    }
}