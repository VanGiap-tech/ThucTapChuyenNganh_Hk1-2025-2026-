using DoanThucTapChuyenNganh.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoanThucTapChuyenNganh.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public IActionResult formAddCategory()
        {
            return View();
        }
        public IActionResult AddCategory(Category c)
        {
            if (string.IsNullOrEmpty(c.Name))
            {
                return View("formAddCategory");
            }
            else
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
          
        }
        [HttpGet]
        public IActionResult formEditCategories(int id)
        {
            var a = db.Categories.Find(id);
            return View(a);
        }
        [HttpPost]
        public IActionResult EditCategories(Category b)
        {
            Category a = db.Categories.Find(b.Id);
            if (a != null)
            {
                a.Name = b.Name;
               

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("!Mã loại sản phẩm chưa có.Không sửa được");
            }
        }

        [HttpGet]
        public IActionResult formRemoveCategories(int id)
        {
            var a = db.Categories.Find(id);
            return View(a);
        }
        [HttpPost]
        public IActionResult RemoveCategories(int id)
        {
            Category a = db.Categories.Find(id);
            if (a != null)
            {

                db.Categories.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return Content("!Mã số loại sản phẩm  này chưa có.Không xóa được");
            }



        }
    }
}
