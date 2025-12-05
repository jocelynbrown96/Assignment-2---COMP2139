using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_1___COMP2139.Models
{
    public class PurchaseEvent
    {
        [Key]
        public int Id { get; set; } // optional if you want a separate key

        // Foreign key to Purchase
        [Required]
        public int PurchaseId { get; set; }

        // Navigation property (nullable to avoid warnings)
        public Purchase? Purchase { get; set; }

        // Foreign key to Event
        [Required]
        public int EventId { get; set; }

        // Navigation property (nullable to avoid warnings)
        public Event? Event { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1; // default 1
    }
}