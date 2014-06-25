using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for BugHub
    /// </summary>
    [HubName("bugs")]
    public class BugHub : Hub
    {
    }
}