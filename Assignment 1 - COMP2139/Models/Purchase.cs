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

        // Navigation property for many-to-many
        public ICollection<PurchaseEvent> PurchaseEvents { get; set; } = new List<PurchaseEvent>();

        // Computed property for total cost
        [NotMapped]
        public decimal TotalCost => PurchaseEvents?.Sum(pe => (pe.Event?.TicketPrice ?? 0) * pe.Quantity) ?? 0;
    }
}