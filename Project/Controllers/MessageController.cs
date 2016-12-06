using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class MessageController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Message
        public ActionResult Index()
        {
            var mess = context.Message.ToList();
            return View(mess);
        }

        public ActionResult NewMessage(string username, string headline, string content)
        {
            Message mess = new Message();
            var i = context.Users.FirstOrDefault(x => x.UserName == username);
            mess.SentTo = i;
            mess.ToUser = mess.SentTo.UserName;
            mess.Headline = headline;
            mess.Content = content;
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(r => r.Id == currentUser);
            mess.SentFrom = user;
            mess.FromUser = user.UserName;
            mess.DateSent = DateTime.Now;
            context.Message.Add(mess);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowMessages()
        {
            var currUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id == currUser);
            var messages = context.Message.Where(x => x.ToUser == user.UserName);
            return View(messages);
        }
    }
}