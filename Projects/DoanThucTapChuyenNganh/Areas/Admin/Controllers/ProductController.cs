using DoanThucTapChuyenNganh.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DoanThucTapChuyenNganh.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<IActionResult> Index()
        {
            var products = await context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult formCreateProduct()
        {
            ViewBag.Categories = db.Categories.ToList(); 
            ViewBag.Brands = db.Brands.ToList();

            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile? imageFile)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Brand");
            ModelState.Remove("OrderDetails");

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                
                    string fileName = Path.GetFileName(imageFile.FileName);

                    
                    string folderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "products");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                   
                    string filePath = Path.Combine(folderPath, fileName);

                   
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    
                    product.ImageUrl = fileName;
                }

                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Brands = context.Brands.ToList();
            return View("formCreateProduct", product);
        }
        [HttpGet]
        public IActionResult formEditProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }


            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");

            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            Product p1 = db.Products.Find(p.Id);
            if (p1 != null)
            {
                p1.Name = p.Name;
                p1.Price = p.Price;
                p1.Description = p.Description;
                p1.StockQuantity = p.StockQuantity;
                p1.Material = p.Material;
                p1.Weight = p.Weight;
                p1.BrandId = p.BrandId;
                p1.CategoryId = p.CategoryId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("!Mã số nhà sản xuất chưa có");
            }
        }
        [HttpGet]
        public IActionResult formDeleteProduct(int id)
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            var a = db.Products.Find(id);
            return View(a);
        }
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
        
            var product = await context.Products.FindAsync(id);

            if (product != null)
            {
                
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", "products", product.ImageUrl);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
