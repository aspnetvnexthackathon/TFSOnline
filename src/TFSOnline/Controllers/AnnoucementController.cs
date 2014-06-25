using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using TFSOnline.Models;

namespace TFSOnline.Controllers
{
    [Microsoft.AspNet.Mvc.Authorize("CanMakeAnnouncement", "true")]
    public class AnnouncementController : Controller
    {
        private readonly TFSOnlineContext db;
        private IHubContext _abhub;

        public AnnouncementController(TFSOnlineContext context, IConnectionManager connectionManager)
        {
            db = context;
            _abhub = connectionManager.GetHubContext<AnnoucementBugHub>();
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
            Announcement a = new Announcement() { Message = message };
            db.Announcements.Add(a);
            db.SaveChanges();

            _abhub.Clients.All.updateAnnouncements(new GlobalAnnoucementViewModel() { LastAnnouncement = a.Message });

            return RedirectToAction("Index", "Dashboard");
        }
    }
}