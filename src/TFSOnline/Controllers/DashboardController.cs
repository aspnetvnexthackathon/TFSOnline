using System.Linq;
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
            string userName = Context.User.Identity.Name;
            BugsViewModel viewModel = new BugsViewModel();
            var allBugs = db.Bugs;

            //Get total work items
            viewModel.TotalWorkItemsCount = allBugs.Where(b => b.AssignedTo == userName && b.State == BugState.Active).Count();

            //Get Resolved work items
            viewModel.ResolvedWorkItemsCount = allBugs.Where(b => b.AssignedTo == userName && b.State == BugState.Resolved).Count();

            //To-Do : Get Saved queries

            return View(viewModel);
        }
    }
}