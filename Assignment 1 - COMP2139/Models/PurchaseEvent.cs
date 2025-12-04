using System.ComponentModel.DataAnnotations;

namespace Assignment_1___COMP2139.Models
{
    public class PurchaseEvent
    {
        [Key]
        public int Id { get; set; } // optional if you want a separate key
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}