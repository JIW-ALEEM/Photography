using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Photography.Data;
using Photography.Models;

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

        [HttpGet]
        public IActionResult PhotoCtg()
        {
            return View();
        }

        // PhotoCategory Action Method
        //[HttpPost]
        //public IActionResult PhotoCtg(PhotoCategory ctg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Add(ctg);
        //        db.SaveChanges();
        //        TempData["Message"] = "User Registered Successfully..";
        //        //return RedirectToAction(nameof(FetchCtg));
        //    }
        //    return View();
        //}


    }
}
