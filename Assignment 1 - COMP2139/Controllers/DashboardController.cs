using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1___COMP2139.Controllers
{
    // Only logged-in users can access anything in this controller
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: /Dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}