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
        private IHubContext _hub;
        private readonly TFSOnlineContext db;

        public BugsController(IConnectionManager connectionManager, TFSOnlineContext context)
	    {
            _hub = connectionManager.GetHubContext<BugHub>();
            db = context;
	    }
        public IEnumerable<Bug> Get()
        {
            return db.Bugs;
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

        }

        public Bug Put(int id, Bug bug)
        {
            var bugToUpdate = db.Bugs.First(b => b.BugId == id);
            bugToUpdate = bug;
            db.SaveChanges();

            return bug;
        }

        public void Delete(int id)
        {
            var bug = db.Bugs.First(b => b.BugId == id);
            db.Bugs.Remove(bug);
            db.SaveChanges();
        }
    }
}