using Microsoft.AspNetCore.Mvc;

namespace Photography.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}
