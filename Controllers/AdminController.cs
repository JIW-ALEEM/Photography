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
        public IActionResult PhotoCtg(PhotoCategory Ctg, IFormFile CategoryPhoto)
        {

            if (CategoryPhoto != null && CategoryPhoto.Length > 0)
            {
                var filename = Path.GetFileName(CategoryPhoto.FileName);
                string folderPath = Path.Combine("wwwroot/img/Category", filename);
                var dbpath = Path.Combine("img/Category", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    CategoryPhoto.CopyTo(stream);
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
