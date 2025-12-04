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
        public required InputModel Input { get; set; }

        public class InputModel(string email, string password)
        {
            [Required]
            [EmailAddress]
            public required string Email { get; set; } = email;

            [Required]
            [DataType(DataType.Password)]
            public required string Password { get; set; } = password;

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public required string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                Log.Information("User registered: {Email}", Input.Email);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect("~/");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}