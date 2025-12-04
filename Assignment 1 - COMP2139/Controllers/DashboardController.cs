using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1___COMP2139.Controllers
{
    [Authorize] // Only logged-in users can access this
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}