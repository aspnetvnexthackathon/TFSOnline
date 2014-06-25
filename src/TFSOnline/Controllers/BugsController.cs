using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using TFSOnline.Models;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for BugsController
    /// </summary>
    public class BugsController : Controller
    {
        private IHubContext _bugshub;
        private IHubContext _abhub;
        private readonly TFSOnlineContext db;

        public BugsController(IConnectionManager connectionManager, TFSOnlineContext context)
	    {
            _bugshub = connectionManager.GetHubContext<BugPerUserHub>();
            _abhub = connectionManager.GetHubContext<AnnoucementBugHub>();
            db = context;
	    }

        public IEnumerable<Bug> Get()
        {
            return db.Bugs;
        }

        public Bug Get(int id)
        {
            return db.Bugs.SingleOrDefault(b => b.BugId == id);
        }

        public IEnumerable<Bug> Get(string username, string bugState)
        {
            if (String.IsNullOrEmpty(bugState))
            {
                return db.Bugs.Where(b => b.AssignedTo == username);
            }
            else
            {

                return db.Bugs.Where(b => b.AssignedTo == username && b.State.ToString() == bugState);
            }

        }

        // Web API expects primitives coming from the request body to have no key value (e.g. '') - they should be encoded, then as '=value'
        public void Post(Bug bug)
        {
            db.Bugs.Add(bug);
            db.SaveChanges();

            //Get last created annoucement - Needs sorting
            string lastAnnouncement = db.Announcements.LastOrDefault().Message;
            
            _abhub.Clients.All.updateAnnouncements(new GlobalAnnoucementViewModel() { BugId = bug.BugId, LastAnnouncement = lastAnnouncement});
	}	
       

        [HttpGet]
        public IActionResult Edit(int? id = null)
        {
            Bug model = null;
            if (id != null)
            {
                model = Get(id.Value);
            }

            if (model==null)
            {
                model = new Bug();
                model.BugId = -1;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bug bug)
        {
            if (bug.BugId == -1)
            {
                db.Bugs.Add(bug);
            }
            else
            {
                db.Bugs.Update(bug);
            }
            db.SaveChanges();

            BugsViewModel viewModel = new BugsViewModel();
            var allBugs = db.Bugs;

            //Get total work items
            viewModel.TotalWorkItemsCount = allBugs.Where(b => b.AssignedTo == bug.AssignedTo && b.State == BugState.Active).Count();
            //Get Resolved work items
            viewModel.ResolvedWorkItemsCount = allBugs.Where(b => b.AssignedTo == bug.AssignedTo && b.State == BugState.Resolved).Count();

            _bugshub.Clients.Group(bug.AssignedTo).updateBugs(viewModel);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}