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
        //Fields that can be populated and sent back to the view
        private ApplicationUser user;
        private ApplicationUser secondUser;
        private List<ApplicationUser> users;
        private List<Message> messages;
        //---------------------------------------

        // Returns all the messages from the database
        //Dont use it atm, remove?
        [Authorize]
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                messages = context.Message.ToList();
                return View(messages);
            }
        }

        //Posts a new message and saves it in the database
        //Only for logged in users
        [Authorize]
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

        //returns all the users from the site so that the logged in user can send them a message and see conversations(Action: ShowConversation)
        //Only for logged in users
        [Authorize]
        public ActionResult ShowMessages()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.ToList();
                return View(users);
            }
        }

        //Shows a conversation beetween the logged in user and the user he/she selected from the list
        //Only for logged in users
        [Authorize]
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

        //Logged in user can search for a specific user to see their conversation
        //Only for logged in users
        [Authorize]
        public ActionResult Search(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.Where(x => x.UserName.Contains(username)).ToList();
                return PartialView("_PartialMessages", users);
            }
        }

        //Returns users so logged in user can see conversations and send messages
        //Only for logged in users
        public ActionResult Message()
        {
            if (User.Identity.IsAuthenticated)
            {

            
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                users = context.Users.ToList();
                return View(users);
            }
            }
            else
            {
                ViewBag.ErrorMessage = "You need to be logged in to see messages";
                    return View();
            }
        }

        //Shows the conversation beetween logged in user and the selected user
        //Only for logged in users
        [Authorize]
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
                //In case there is no conversation to be shown, an errormessage is displayed
                ViewBag.ErrorMessage = "A conversation beetween you and " + username + " does not exist!";
                return PartialView("_PartialConv");
            }
        }

        //Form for writing a new message
        //Only for logged in users
        [Authorize]
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