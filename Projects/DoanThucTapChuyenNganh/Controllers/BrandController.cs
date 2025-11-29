
using DoanThucTapChuyenNganh.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoanThucTapChuyenNganh.Controllers
{
    public class BrandController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext(); 
        
        public IActionResult Brands()
        {

            return View(db.Brands.ToList());
        }

        [HttpGet]
        public IActionResult formAddBrands()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBrands(Brand b)
        {
            if (string.IsNullOrEmpty(b.Name))
            {
                return View();
            }
            else
            {
                db.Brands.Add(b);
                db.SaveChanges();
                return RedirectToAction("Brands");
            }
        }
        [HttpGet]
        public IActionResult formEditBrands(int id)
        {
            var a = db.Brands.Find(id);
            return View(a);
        }
        [HttpPost]
        public IActionResult EditBrands(Brand b)
        {
            Brand a = db.Brands.Find(b.Id);
            if (a != null)
            {
                a.Name = b.Name;
                a.Country = b.Country;

                db.SaveChanges();
                return RedirectToAction("Brands");
            }
            else
            {
                return Content("!Mã số nhà sản xuất chưa có");
            }
        }

        public IActionResult RemoveBrands(int id)
        {
            Brand a = db.Brands.Find(id);
            if (a != null)
            {

                db.Brands.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Brands");

            }
            else
            {
                return Content("!Mã số nhà sản xuất này chưa có.Không xóa được");
            }



        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(db.Brands.ToList());
        }

    }
}
