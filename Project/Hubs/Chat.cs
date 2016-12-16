using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Hubs
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
                var test = Context.User.Identity.Name;
            Clients.All.newMessage(
                    test + ": " + message);
        }
    }
}