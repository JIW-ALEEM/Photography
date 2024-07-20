using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult PhotoCtg()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult FetchPhotoCtg()
        {
            return View(db.PhotoCategories.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeletePhotoCtg(int? id)
        {
            var data = db.PhotoCategories.FirstOrDefault(x => x.CategoryId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPhotoCtg));
        }

        // Update Photo Ctg view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdatePhotoCtg(int? id)
        {
            var data = db.PhotoCategories.FirstOrDefault(x => x.CategoryId == id);
            return View(data);
        }

        // Update Photo Ctg action method
        [HttpPost]
        public IActionResult UpdatePhotoCtg(PhotoCategory UpdatePhotoCtg1, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/img/Category");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("img/Category", unique_name);
                UpdatePhotoCtg1.CategoryPhoto = dbPath;
                db.Update(UpdatePhotoCtg1);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchPhotoCtg));
            }
            else
            {
                var existingctg = db.PhotoCategories.FirstOrDefault(p => p.CategoryId == UpdatePhotoCtg1.CategoryId);
                if (existingctg != null)
                {
                    UpdatePhotoCtg1.CategoryPhoto = existingctg.CategoryPhoto;

                    // Detach existing tracked entity
                    db.Entry(existingctg).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchPhotoCtg));
                }
            }

            // Update entity state
            db.Update(UpdatePhotoCtg1);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchPhotoCtg));
        }
    }
}
