using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment_1___COMP2139.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Existing tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseEvent> PurchaseEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important: Identity requires this

            // Configure many-to-many: Purchase <-> Event via PurchaseEvent
            modelBuilder.Entity<PurchaseEvent>()
                .HasKey(pe => new { pe.PurchaseId, pe.EventId });

            modelBuilder.Entity<PurchaseEvent>()
                .HasOne(pe => pe.Purchase)
                .WithMany(p => p.PurchaseEvents)
                .HasForeignKey(pe => pe.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseEvent>()
                .HasOne(pe => pe.Event)
                .WithMany(e => e.PurchaseEvents)
                .HasForeignKey(pe => pe.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Concerts", Description = "Live music performances" },
                new Category { Id = 2, Name = "Sports", Description = "Sporting events and games" }
            );

            // Seed events
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Title = "Adele Live", Location = "London, UK", Date = DateTime.UtcNow.AddDays(10), TicketPrice = 150.00M, AvailableTickets = 50, CategoryId = 1 },
                new Event { Id = 2, Title = "The Weeknd World Tour", Location = "Toronto, Canada", Date = DateTime.UtcNow.AddDays(15), TicketPrice = 120.00M, AvailableTickets = 100, CategoryId = 1 },
                new Event { Id = 3, Title = "Coldplay Stadium Show", Location = "Paris, France", Date = DateTime.UtcNow.AddDays(20), TicketPrice = 110.00M, AvailableTickets = 200, CategoryId = 1 },
                new Event { Id = 4, Title = "Taylor Swift: The Eras Tour", Location = "New York, USA", Date = DateTime.UtcNow.AddDays(25), TicketPrice = 250.00M, AvailableTickets = 75, CategoryId = 1 },
                new Event { Id = 5, Title = "BTS Reunion", Location = "Seoul, South Korea", Date = DateTime.UtcNow.AddDays(30), TicketPrice = 180.00M, AvailableTickets = 150, CategoryId = 1 },
                new Event { Id = 6, Title = "Ed Sheeran Acoustic Night", Location = "Dublin, Ireland", Date = DateTime.UtcNow.AddDays(35), TicketPrice = 95.00M, AvailableTickets = 120, CategoryId = 1 },
                new Event { Id = 7, Title = "Champions League Final", Location = "Berlin, Germany", Date = DateTime.UtcNow.AddDays(40), TicketPrice = 300.00M, AvailableTickets = 200, CategoryId = 2 },
                new Event { Id = 8, Title = "NBA All-Star Game", Location = "Los Angeles, USA", Date = DateTime.UtcNow.AddDays(45), TicketPrice = 350.00M, AvailableTickets = 180, CategoryId = 2 },
                new Event { Id = 9, Title = "Wimbledon Finals", Location = "London, UK", Date = DateTime.UtcNow.AddDays(50), TicketPrice = 220.00M, AvailableTickets = 80, CategoryId = 2 },
                new Event { Id = 10, Title = "Super Bowl LX", Location = "Las Vegas, USA", Date = DateTime.UtcNow.AddDays(55), TicketPrice = 500.00M, AvailableTickets = 300, CategoryId = 2 },
                new Event { Id = 11, Title = "Formula 1 Grand Prix", Location = "Monaco", Date = DateTime.UtcNow.AddDays(60), TicketPrice = 400.00M, AvailableTickets = 250, CategoryId = 2 },
                new Event { Id = 12, Title = "Olympics Opening Ceremony", Location = "Tokyo, Japan", Date = DateTime.UtcNow.AddDays(65), TicketPrice = 450.00M, AvailableTickets = 500, CategoryId = 2 }
            );
        }
    }
}