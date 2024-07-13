using Microsoft.AspNetCore.Mvc;
using Photography.Data;

namespace Photography.Controllers
{
    public class AdminController : Controller
    {
        private readonly PhotographyContext db;

        public AdminController(PhotographyContext db)
        {
            this.db = db;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}
