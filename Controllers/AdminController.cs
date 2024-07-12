using Microsoft.AspNetCore.Mvc;

namespace Photography.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
