using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult CommentAdd() 
        {
            return View();
        }


        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            ViewBag.name = name;
            ViewBag.comment = comment;

            return View();
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("Email", "admin@admin.com");
            HttpContext.Response.Cookies.Append("Password", "Admin123");
            return View();
        }

        public IActionResult Privacy()
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
