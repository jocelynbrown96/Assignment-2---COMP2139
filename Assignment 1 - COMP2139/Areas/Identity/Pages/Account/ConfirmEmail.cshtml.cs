using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class ConfirmEmailModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string? userId, string? code)
        {
            if (userId == null || code == null) return RedirectToPage("/Index");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound($"Unable to load user.");

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                Log.Information("Email confirmed for {Email}", user.Email);
            }
            return Page();
        }
    }
}