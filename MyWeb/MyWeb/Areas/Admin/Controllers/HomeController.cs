using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Trang chủ";
            return View();
        }
        public IActionResult MarkersManager()
        {
            ViewData["Title"] = "Quản lý Marker";
            return View();
        }
        public IActionResult CharactersManager()
        {
            ViewData["Title"] = "Quản lý nhân vật";
            return View();
        }
        public IActionResult BackgroundsManager()
        {
            ViewData["Title"] = "Quản lý Background";
            return View();
        }
    }
}
