using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Photography.Data;
using Photography.Models;
using System.Diagnostics;

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

        //PhotoCategory Action Method
        [HttpPost]
        public IActionResult PhotoCtg(PhotoCategory Ctg, IFormFile CtgImg)
        {

            if (CtgImg != null && CtgImg.Length > 0)
            {
                var filename = Path.GetFileName(CtgImg.FileName);
                string folderPath = Path.Combine("wwwroot/img/PhotoCategory", filename);
                var dbpath = Path.Combine("img/PhotoCategory", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    CtgImg.CopyTo(stream);
                }
                Ctg.CategoryPhoto = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Ctg);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchPhotoCtg));
                }
            }
            else
            {
                ModelState.AddModelError("CtgImg", "Category Image field is Required");
            }
            return View();
        }

        public IActionResult FetchPhotoCtg()
        {
            return View(db.PhotoCategories.ToList());
        }

    }
}
