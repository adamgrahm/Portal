using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private ApplicationUser user;
        private ApplicationUser secondUser;
        private List<ApplicationUser> users;
        private List<Message> messages;
        // GET: Message
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                messages = context.Message.ToList();
                return View(messages);
            }
        }


        [HttpPost]
        public ActionResult NewMessage(string username, string content)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Message mess = new Message();
                user = context.Users.FirstOrDefault(x => x.UserName == username);
                mess.SentTo = user;
                mess.ToUser = mess.SentTo.UserName;
                //mess.Headline = headline;
                mess.Content = content;
                var currentUser = User.Identity.GetUserId();
                secondUser = context.Users.FirstOrDefault(r => r.Id == currentUser);
                mess.SentFrom = secondUser;
                mess.FromUser = secondUser.UserName;
                mess.Identifier = user.UserName + secondUser.UserName;
                mess.DateSent = DateTime.Now;
                if (ModelState.IsValid)
                {
                    context.Message.Add(mess);
                    context.SaveChanges();
                }
                
                return PartialView("_PartialConv");
            }
        }

        public ActionResult ShowMessages()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.ToList();
                return View(users);
            }
        }

        public ActionResult ShowConversation(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                messages = context.Message.Where(x => x.Identifier == user.UserName + username || x.Identifier == username + user.UserName).ToList();
                if (messages.Count() > 0)
                {
                    return View(messages);
                }
                ViewBag.ErrorMessage = "A conversation beetween you and " + username + " does not exist!";
                return View();
            }
        }

        public ActionResult Search(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.Where(x => x.UserName.Contains(username)).ToList();
                return PartialView("_PartialMessages", users);
            }
        }

        public ActionResult Message()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.ToList();
                return View(users);
            }
        }

        [HttpPost]
        public ActionResult PartialConversation(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                messages = context.Message.Where(x => x.Identifier == user.UserName + username || x.Identifier == username + user.UserName).ToList();
                if (messages.Count() > 0)
                {
                    return PartialView("_PartialConv", messages);
                }
                ViewBag.ErrorMessage = "A conversation beetween you and " + username + " does not exist!";
                return PartialView("_PartialConv");
            }
        }

        public ActionResult ReturnForm()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.ToList();
                return PartialView("_MessageForm", users);
            }
        }
    }
}