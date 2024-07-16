using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Photography.Data;
using Photography.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Photography.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhotographyContext _db;


        public HomeController(PhotographyContext db)
        {
            _db = db;
        }
        // Signup View
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, IFormFile UserImg)
        {
            if (UserImg == null || UserImg.Length == 0)
            {
                // Set default image path
                user.UserImg = "img/User/default.png";
            }
            else
            {
                var filename = Path.GetFileName(UserImg.FileName);
                string folderPath = Path.Combine("wwwroot/img/User/", filename);
                var dbpath = Path.Combine("img/User/", filename);

                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    UserImg.CopyTo(stream);
                }

                user.UserImg = dbpath;
            }

            user.UserRoleId = 2;
            _db.Add(user);
            _db.SaveChanges();

            TempData["Message"] = "Record Inserted Successfully";
            return RedirectToAction(nameof(Login));
        }



        // Signup Action Method
        //[HttpPost]
        //public IActionResult SignUp(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.UserRoleId = 2;
        //        _db.Add(user);
        //        _db.SaveChanges();
        //        TempData["Message"] = "User Registered Successfully..";
        //        return RedirectToAction(nameof(Login));
        //    }
        //    return View();
        //}

        // Login Action Method
        public IActionResult Login(User user)
        {
            var data = _db.Users.FirstOrDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (data != null)
            {
                if (data.UserRoleId == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, data.UserName),
                        new Claim(ClaimTypes.Email, data.UserEmail),
                        new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
                        new Claim("UserImg", data.UserImg),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, data.UserName),
                        new Claim(ClaimTypes.Email, data.UserEmail),
                        new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
                        new Claim("UserImg", data.UserImg),
                        new Claim(ClaimTypes.Role, "User")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (data.UserRoleId == 1)
                    {
                        return RedirectToAction("AdminIndex", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        // Logout Action Method
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }
        
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Blog_details()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Elements()
        {
            return View();
        }
   
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Plan()
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
