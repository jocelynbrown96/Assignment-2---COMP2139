using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_1___COMP2139.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            // Redirect wherever you want after logout
            if (returnUrl != null)
                return LocalRedirect(returnUrl);

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult OnGet()
        {
            // Prevent GET requests from showing a logout page
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}