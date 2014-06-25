using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for BugsController
    /// </summary>
    public class BugsController : Controller
    {
        private IHubContext _hub;

	    public BugsController(IConnectionManager connectionManager)
	    {
            _hub = connectionManager.GetHubContext<BugHub>();
	    }

        [HttpGet]
        public IActionResult New()
        {
            Bug model = new Bug();
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Bug bug)
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}