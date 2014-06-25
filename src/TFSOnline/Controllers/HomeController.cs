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
            // Get most popular albums
            return View();
        }
    }
}