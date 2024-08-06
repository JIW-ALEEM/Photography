using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using NuGet.Protocol.Plugins;
using Photography.Data;
using Photography.Models;
using System.Diagnostics;

namespace Photography.Controllers
{
    public class AdminController : Controller
    {
        private readonly PhotographyContext _db;

        public AdminController(PhotographyContext db)
        {
            this._db = db;
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
                    _db.Add(Ctg);
                    _db.SaveChanges();
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
            return View(_db.PhotoCategories.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeletePhotoCtg(int? id)
        {
            var data = _db.PhotoCategories.FirstOrDefault(x => x.CategoryId == id);
            _db.Remove(data);
            _db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPhotoCtg));
        }

        // Update Photo Ctg view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdatePhotoCtg(int? id)
        {
            var data = _db.PhotoCategories.FirstOrDefault(x => x.CategoryId == id);
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
                _db.Update(UpdatePhotoCtg1);
                _db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchPhotoCtg));
            }
            else
            {
                var existingctg = _db.PhotoCategories.FirstOrDefault(p => p.CategoryId == UpdatePhotoCtg1.CategoryId);
                if (existingctg != null)
                {
                    UpdatePhotoCtg1.CategoryPhoto = existingctg.CategoryPhoto;

                    // Detach existing tracked entity
                    _db.Entry(existingctg).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchPhotoCtg));
                }
            }

            // Update entity state
            _db.Update(UpdatePhotoCtg1);
            _db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchPhotoCtg));
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Photos()
        {
            ViewBag.PhotoCtg = new SelectList(_db.PhotoCategories, "CategoryId", "CategoryName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Photos(Photo Img2, IFormFile PhotoUrl)
        {

            if (PhotoUrl != null && PhotoUrl.Length > 0)
            {
                var filename = Path.GetFileName(PhotoUrl.FileName);
                string folderPath = Path.Combine("wwwroot/img/Photo", filename);
                var dbpath = Path.Combine("img/Photo", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    PhotoUrl.CopyTo(stream);
                }
                Img2.PhotoUrl = dbpath;
             
                    _db.Add(Img2);
                    _db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchPhoto));
                
            }
            else
            {
                ModelState.AddModelError("Img", "Category Image field is Required");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult FetchPhoto()
        {
            return View(_db.Photos.Include("Category").ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeletePhoto(int? id)
        {
            var data = _db.Photos.FirstOrDefault(x => x.PhotoId == id);
            _db.Remove(data);
            _db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPhoto));
        }

        // Update Photo Ctg view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdatePhoto(int? id)
        {
            var data = _db.Photos.FirstOrDefault(x => x.PhotoId == id);
            ViewBag.PhotoCtg = new SelectList(_db.PhotoCategories, "CategoryId", "CategoryName");
            return View(data);
        }

        // Update Photo Ctg action method
        [HttpPost]
        public IActionResult UpdatePhoto(Photo UpdatePhoto1, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/img/Photo");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("img/Photo", unique_name);
                UpdatePhoto1.PhotoUrl = dbPath;
                _db.Update(UpdatePhoto1);
                _db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchPhoto));
            }
            else
            {
                var existingImg = _db.Photos.FirstOrDefault(p => p.PhotoId == UpdatePhoto1.PhotoId);
                if (existingImg != null)
                {
                    UpdatePhoto1.PhotoUrl = existingImg.PhotoUrl;

                    // Detach existing tracked entity
                    _db.Entry(existingImg).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchPhoto));
                }
            }

            // Update entity state
            _db.Update(UpdatePhoto1);
            _db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchPhoto));
        }

    }
}
