﻿using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Linq;
using TFSOnline.Models;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for QueryController
    /// </summary>
    [Authorize]
    public class QueryController : Controller
    {
        private readonly TFSOnlineContext db;

        public QueryController(TFSOnlineContext context)
        {
            db = context;
        }

        //
        // GET: /Query/QueryBugs
        public IActionResult QueryBugs(string assignedTo = null, BugState bugState = BugState.Active)
        {
            ViewBag.AssignedTo = new SelectList(db.Users, "UserName", "UserName", assignedTo);
            ViewBag.bugState = new SelectList(Enum.GetNames(typeof(BugState)), bugState.ToString());

            var bugs = (assignedTo == null) ?
            db.Bugs.Where(b => b.State == bugState).ToList() :
            db.Bugs.Where(b => b.AssignedTo == assignedTo && b.State == bugState).ToList();

            return View(bugs);
        }
    }
}