using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class LoginModel(SignInManager<ApplicationUser> signInManager) : PageModel
    {
        [BindProperty]
        public required InputModel Input { get; set; }

        public class InputModel(string email, string password, bool rememberMe)
        {
            [Required]
            [EmailAddress]
            public required string Email { get; set; } = email;

            [Required]
            [DataType(DataType.Password)]
            public required string Password { get; set; } = password;

            public bool RememberMe { get; } = rememberMe;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                Log.Information("User logged in: {Email}", Input.Email);
                return LocalRedirect("~/");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            Log.Warning("Failed login attempt: {Email}", Input.Email);
            return Page();
        }
    }
}