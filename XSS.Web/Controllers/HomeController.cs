using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javascriptEncoder;
        private UrlEncoder _urlEncoder;

        public HomeController(ILogger<HomeController> logger, HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder)
        {
            _logger = logger;
            _htmlEncoder = htmlEncoder;
            _javascriptEncoder = javaScriptEncoder;
            _urlEncoder = urlEncoder;
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

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {

            string encodedName = _urlEncoder.Encode(name);

            ViewBag.name = name;
            ViewBag.comment = comment;

            System.IO.File.AppendAllText("Comment.txt", $"{name} - {comment}\n");

            return RedirectToAction();
        }

        public IActionResult login(string returnUrl="/")
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult login(string email, string password)
        {
            string retunrUrl = TempData["returnUrl"].ToString();
            //email ve password kontrolü yapılıyor

            if (Url.IsLocalUrl(retunrUrl)) 
            {
                return Redirect(retunrUrl);

            }
            return Redirect("/");
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
