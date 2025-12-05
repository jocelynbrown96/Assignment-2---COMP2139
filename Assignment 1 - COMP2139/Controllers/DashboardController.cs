using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1___COMP2139.Controllers
{
    [Authorize] // User must be logged in
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}