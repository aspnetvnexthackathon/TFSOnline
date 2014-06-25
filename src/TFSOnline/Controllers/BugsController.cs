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
        public IActionResult Edit(int? id = null)
        {
            // TODO: 
            // if Id is not null, then get the existing bug
            // else pass a new bug model

            Bug model = new Bug();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Bug bug)
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}