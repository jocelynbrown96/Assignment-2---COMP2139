using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;

namespace Assignment_1___COMP2139.Assignment1.TestRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Running tests...\n");
            AllModelTests.RunAll();
            Console.WriteLine("\nAll tests completed.");
        }
    }

    public static class AllModelTests
    {
        public static void RunAll()
        {
            RunTest("Event_WithValidData_IsValid", Event_WithValidData_IsValid);
            RunTest("Event_WithoutTitle_IsInvalid", Event_WithoutTitle_IsInvalid);
            RunTest("Event_WithNegativeTicketPrice_IsInvalid", Event_WithNegativeTicketPrice_IsInvalid);

            RunTest("Purchase_WithValidData_IsValid", Purchase_WithValidData_IsValid);
            RunTest("Purchase_WithoutGuestName_IsInvalid", Purchase_WithoutGuestName_IsInvalid);
            RunTest("Purchase_WithoutGuestEmail_IsInvalid", Purchase_WithoutGuestEmail_IsInvalid);
        }

        private static void RunTest(string testName, Func<bool> testFunc)
        {
            try
            {
                bool result = testFunc();
                Console.WriteLine(result ? $"[PASS] {testName}" : $"[FAIL] {testName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {testName}: {ex.Message}");
            }
        }

        // ===== Event Tests =====
        private static bool Event_WithValidData_IsValid()
        {
            var model = new Event
            {
                Id = 1,
                Title = "Test Event",
                Location = "Toronto",
                Date = DateTime.Now.AddDays(5),
                TicketPrice = 50,
                AvailableTickets = 100,
                CategoryId = 1
            };
            return IsValid(model);
        }

        private static bool Event_WithoutTitle_IsInvalid()
        {
            var model = new Event
            {
                Id = 2,
                Title = null,
                Location = "Toronto",
                Date = DateTime.Now.AddDays(5),
                TicketPrice = 50,
                AvailableTickets = 10,
                CategoryId = 1
            };
            return !IsValid(model);
        }

        private static bool Event_WithNegativeTicketPrice_IsInvalid()
        {
            var model = new Event
            {
                Id = 3,
                Title = "Bad Event",
                Location = "Toronto",
                Date = DateTime.Now.AddDays(5),
                TicketPrice = -10,
                AvailableTickets = 10,
                CategoryId = 1
            };
            return !IsValid(model);
        }

        // ===== Purchase Tests =====
        private static bool Purchase_WithValidData_IsValid()
        {
            var user = new ApplicationUser { Id = "1", UserName = "johnuser", FullName = "John Doe" };

            var model = new Purchase
            {
                Id = 1,
                GuestName = "John Doe",
                GuestEmail = "john@example.com",
                UserId = user.Id,
                User = user,
                PurchaseEvents = new List<PurchaseEvent>
                {
                    new PurchaseEvent
                    {
                        Event = new Event { Id = 1, Title = "Sample Event", TicketPrice = 50, AvailableTickets = 10 },
                        Quantity = 2
                    }
                }
            };
            return IsValid(model);
        }

        private static bool Purchase_WithoutGuestName_IsInvalid()
        {
            var user = new ApplicationUser { Id = "2", UserName = "janeuser", FullName = "Jane Doe" };

            var model = new Purchase
            {
                Id = 2,
                GuestName = null,
                GuestEmail = "jane@example.com",
                UserId = user.Id,
                User = user,
                PurchaseEvents = new List<PurchaseEvent>()
            };
            return !IsValid(model);
        }

        private static bool Purchase_WithoutGuestEmail_IsInvalid()
        {
            var user = new ApplicationUser { Id = "3", UserName = "bobuser", FullName = "Bob Smith" };

            var model = new Purchase
            {
                Id = 3,
                GuestName = "Bob Smith",
                GuestEmail = null,
                UserId = user.Id,
                User = user,
                PurchaseEvents = new List<PurchaseEvent>()
            };
            return !IsValid(model);
        }

        // ===== Helper =====
        private static bool IsValid(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }
    }
}