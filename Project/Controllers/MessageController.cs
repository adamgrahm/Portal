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


        [HttpPost]
        public ActionResult NewMessage(string username, string content)
        {
            Message mess = new Message();
            var i = context.Users.FirstOrDefault(x => x.UserName == username);
            
            mess.SentTo = i;
            mess.ToUser = mess.SentTo.UserName;
            //mess.Headline = headline;
            mess.Content = content;
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(r => r.Id == currentUser);
            mess.SentFrom = user;
            mess.FromUser = user.UserName;
            mess.Identifier = i.UserName + user.UserName;
            mess.DateSent = DateTime.Now;
            context.Message.Add(mess);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowMessages()
        {
            var i= context.Users.ToList();
            return View(i);
        }

        public ActionResult ShowConversation(string username)
        {
            var currUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id == currUser);
            var i = context.Message.Where(x => x.Identifier == user.UserName + username || x.Identifier == username + user.UserName);
            if (i.Count() > 0)
            {
                return View(i);
            }
            ViewBag.ErrorMessage = "A conversation beetween you and " + username + " does not exist!";
            return View();
        }

        public ActionResult Search(string username)
        {
            var i = context.Users.Where(x => x.UserName.Contains(username));
            return PartialView("_PartialMessages", i);
        }

        public ActionResult Message()
        {
            var i = context.Users.ToList();
            return View(i);
        }
    }
}