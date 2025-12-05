using System.Diagnostics;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1___COMP2139.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult About()
    {
        return View();
    }
    public IActionResult AccessDenied()
    {
        return View("403");
    }

    public IActionResult NotFoundPage()
    {
        return View("404");
    }

    public IActionResult Error404() => View();
}