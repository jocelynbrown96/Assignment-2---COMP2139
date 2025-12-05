using System.ComponentModel.DataAnnotations;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string FullName { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new ApplicationUser {
                UserName = Input.Email,
                Email = Input.Email,
                FullName = Input.FullName
            };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                Log.Information("User registered: {Email}", Input.Email);

                // ⭐ NEW: Assign all new users to the Attendee role
                await _userManager.AddToRoleAsync(user, "Attendee");

                // Generate email confirmation token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                // TEMP — show the confirmation link on screen
                TempData["ConfirmationLink"] = confirmationUrl;

                return RedirectToPage("Register");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}