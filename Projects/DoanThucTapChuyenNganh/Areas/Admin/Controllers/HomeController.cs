
using DoanThucTapChuyenNganh.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using Microsoft.AspNetCore.Authentication;

namespace DoanThucTapChuyenNganh.Areas.Admin.Controllers
{

    [Area("Admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   
    public class HomeController :  AdminBaseController
    {
       
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Help()
        {
            return View();
        }
		public IActionResult Settings()
		{
            Console
                .WriteLine("test");
			return View();
		}
        public IActionResult Test()
        {
            return View();
        }
    }
}
