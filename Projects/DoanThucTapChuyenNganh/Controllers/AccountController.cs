using DoanThucTapChuyenNganh.Models;
using DoanThucTapChuyenNganh.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DoanThucTapChuyenNganh.ViewModels.LoginViewModel;
using static DoanThucTapChuyenNganh.ViewModels.RegisterViewModel;

namespace DoanThucTapChuyenNganh.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public class AccountController : Controller
    {   
        private ApplicationDbContext db=new ApplicationDbContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChuyenTrang(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            //db.Users.ToList();



            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == model.Username );
            

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password,user.PasswordHash))
            {
                if (user.IsLocked)
                {
                    ModelState.AddModelError( "","Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin.");
                    return View("Login",model);
                }
                HttpContext.Session.SetString("Username", user.UserName);
                HttpContext.Session.SetString("FullName", user.FullName ?? "User");
                string role = (user.IsAdmin == true) ? "Admin" : "Customer";
                HttpContext.Session.SetString("Role", role);

                if (user.IsAdmin == true)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View("Login", model);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Chống tấn công giả mạo request
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           
                if (!ModelState.IsValid) {
                    return View(model);
                }
                var existUser= await db.Users
                    .FirstOrDefaultAsync(u=>u.UserName==model.Username || u.Email == model.Email);
                if (existUser != null)
                {
                    if (existUser.UserName == model.Username)
                    {
                        ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại!Vui lòng nhập lại.");
                    }
                    if (existUser.Email == model.Email)
                    {
                        ModelState.AddModelError("Email", "Tài khoản email đã tồn tại!Vui lòng nhập lại.");
                    }
                    return View("Register",model);
                }
            try
            {
                var newUser = new User
                {
                    UserName = model.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    FullName = model.FullName,
                    Email = model.Email,
                    CreatedAt= DateTime.Now,
                    IsAdmin = false,
                };
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập."; 
                return RedirectToAction("Login");

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình đăng ký. Vui lòng thử lại.");
                return View(model);
            }

        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Logout()
        {
           
            HttpContext.Session.Clear();

            HttpContext.Session.Remove("Username");

           
            return RedirectToAction("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
    }
}
