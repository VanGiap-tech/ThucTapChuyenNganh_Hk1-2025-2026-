using Microsoft.AspNetCore.Mvc;

namespace DoanThucTapChuyenNganh.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
    }
}
