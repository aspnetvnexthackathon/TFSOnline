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
    }
}