using Microsoft.AspNet.Mvc;
using TFSOnline.Models;

namespace TFSOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly TFSOnlineContext db;

        public HomeController(TFSOnlineContext context)
        {
            db = context;
        }

        //
        // GET: /Home/
        public IActionResult Index()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                // Get most popular albums
                return View();
            }
        }
    }
}