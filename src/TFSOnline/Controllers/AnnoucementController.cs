using Microsoft.AspNet.Mvc;
using TFSOnline.Models;

namespace TFSOnline.Controllers
{
    [Authorize("CanMakeAnnouncement", "true")]
    public class AnnouncementController : Controller
    {
        private readonly TFSOnlineContext db;
        //private IHubContext _hub;

        public AnnouncementController(TFSOnlineContext context/*, IConnectionManager connectionManager*/)
        {
            db = context;
            //_hub = connectionManager.GetHubContext<BugHub>();
        }

        //
        // GET: /Home/
        public IActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/
        [HttpPost]
        public IActionResult MakeAnnouncement(string message)
        {
            //TODO: Insert the announcement into the announcement table and send a message to all clients via signalr.
            return RedirectToAction("Index", "Dashboard");
        }
    }
}