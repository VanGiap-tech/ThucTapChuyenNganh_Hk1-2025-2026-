using Microsoft.AspNetCore.Mvc;

namespace DoanThucTapChuyenNganh.Controllers
{

    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Brands()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
		public IActionResult Settings()
		{
			return View();
		}
        public IActionResult Test()
        {
            return View();
        }
    }
}
