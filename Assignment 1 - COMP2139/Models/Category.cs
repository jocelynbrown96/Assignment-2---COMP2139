using System.ComponentModel.DataAnnotations;

namespace Assignment_1___COMP2139.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Relationships
    public ICollection<Event> Events { get; set; } = new List<Event>();
}