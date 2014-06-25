using Microsoft.AspNet.Mvc;
using System;
using TFSOnline.Models;

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
        [HttpPost]
        public IActionResult QueryBugs(string assignedTo, BugState bugState)
        {
            return View();
        }
    }
}