using Microsoft.AspNet.Mvc;
using System;
using TFSOnline.Models;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for QueryController
    /// </summary>
    public class QueryController : Controller
    {
        private readonly TFSOnlineContext db;

        public QueryController(TFSOnlineContext context)
        {
            db = context;
        }

        //
        // GET: /Home/
        public IActionResult QueryBugs(string assignedTo, BugState bugState)
        {
            ViewBag.AssignedTo = new SelectList(db.Users, "UserName", "UserName", assignedTo);
            ViewBag.bugState = new SelectList(Enum.GetNames(typeof(BugState)), bugState.ToString());
            var bugs = db.Bugs.Where(b => b.AssignedTo == assignedTo && b.State == bugState).ToList();
            return View(bugs);
        }
    }
}