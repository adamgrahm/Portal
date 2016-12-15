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
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Clients.All.newMessage(message);
            }
        }
    }
}