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
            HttpContext.Response.Cookies.Append("Email", "admin@admin.com");
            HttpContext.Response.Cookies.Append("Password", "Admin123");

            if (System.IO.File.Exists("Comment.txt"))
            {
                ViewBag.comments = System.IO.File.ReadAllLines("Comment.txt");
            }

            return View();
        }


        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            ViewBag.name = name;
            ViewBag.comment = comment;

            System.IO.File.AppendAllText("Comment.txt", $"{name} - {comment}\n");

            return RedirectToAction();
        }


        public IActionResult Index()
        {
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
