using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for BugHub
    /// </summary>
    [HubName("bugperuser")]
    public class BugPerUserHub : Hub
    {
        public Task JoinGroup(string userId)
        {
            return Groups.Add(Context.ConnectionId, groupName: userId);
        }

        public Task RemoveGroup(string userId)
        {
            return Groups.Remove(Context.ConnectionId, groupName: userId);
        }
    }
}