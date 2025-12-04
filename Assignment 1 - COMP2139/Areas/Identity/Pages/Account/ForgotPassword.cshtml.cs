using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class ForgotPasswordModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        [BindProperty]
        public required InputModel Input { get; set; }

        public class InputModel(string email)
        {
            [Required]
            [EmailAddress]
            public required string Email { get; set; } = email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user != null && await userManager.IsEmailConfirmedAsync(user))
            {
                Log.Information("Password reset requested: {Email}", Input.Email);
            }

            return RedirectToPage("/Account/ForgotPasswordConfirmation");
        }
    }
}