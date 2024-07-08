using Microsoft.AspNetCore.Mvc;
using Photography.Models;
using System.Diagnostics;

namespace Photography.Controllers
{
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
        public IActionResult about()
        {
            return View();
        }

        public IActionResult gallery()
        {
            return View();
        }
        
        public IActionResult blog()
        {
            return View();
        }
        public IActionResult blog_details()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult elements()
        {
            return View();
        }
   
        public IActionResult services()
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
    }
}
