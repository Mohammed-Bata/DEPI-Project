using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
