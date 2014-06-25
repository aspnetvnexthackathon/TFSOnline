using Microsoft.AspNet.Mvc;
using TFSOnline.Models;

namespace TFSOnline.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly TFSOnlineContext db;

        public DashboardController(TFSOnlineContext context)
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