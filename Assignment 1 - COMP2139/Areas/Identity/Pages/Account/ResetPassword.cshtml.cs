using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class ResetPasswordModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        [BindProperty]
        public required InputModel Input { get; set; }

        public class InputModel(string email, string password, string code, string confirmPassword)
        {
            [Required]
            [EmailAddress]
            public required string Email { get; set; } = email;

            [Required]
            [DataType(DataType.Password)]
            public required string Password { get; set; } = password;

            [DataType(DataType.Password)]
            [Compare("Password")]
            public required string ConfirmPassword { get; set; } = confirmPassword;

            public required string Code { get; set; } = code;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user == null) return RedirectToPage("/Index");

            var result = await userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                Log.Information("Password reset for user {Email}", Input.Email);
                return RedirectToPage("/Account/Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}