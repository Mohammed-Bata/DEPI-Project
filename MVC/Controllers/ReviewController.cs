using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
