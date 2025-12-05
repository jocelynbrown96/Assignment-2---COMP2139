using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _signInManager.PasswordSignInAsync(
                Input.Email,
                Input.Password,
                Input.RememberMe,
                lockoutOnFailure: false
            );

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