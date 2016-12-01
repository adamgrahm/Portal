using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Forum
        public ActionResult Index()
        {
            var threads = context.Thread.ToList();
            return View(threads);
        }

        [HttpPost]
        public ActionResult Index(string headline)
        {
            var newThread = new Thread();
            newThread.DatePosted = DateTime.Now;
            newThread.Headline = headline;
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(r => r.Id == currentUser);
            newThread.PostedBy = user.NickName;
            context.Thread.Add(newThread);
            context.SaveChanges();
            return PartialView("_PartialForum",context.Thread.ToList());
        }

        public ActionResult SelectedPost(int id)
        {
            var whichThread = context.Thread.Single(u => u.Id == id);
            if (whichThread.Id == id)
            {
                return View(whichThread);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult NewForumPost(string headline, string forumpost, int id)
        {
            var newPost = new ThreadPost();
            newPost.DatePosted = DateTime.Now;
            newPost.ForumPost = forumpost;
            var findThread = context.Thread.FirstOrDefault(u => u.Id == id);
            newPost.Thread = findThread;
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == currentUser);
            newPost.PostedBy = user.NickName;
            
            context.ThreadPost.Add(newPost);
            context.SaveChanges();
            return RedirectToRoute("Test", new { Id = id });
        }

        [HttpPost]
        public ActionResult ReplyToPost(string reply, int id)//<---ThreadpostId
        {
            ForumReplies replies = new ForumReplies();
            var test = context.ThreadPost.Where(u => u.Id == id).Select(y => y.Thread.Id).FirstOrDefault();
            var i = context.ThreadPost.FirstOrDefault(u => u.Id == id);
            replies.Threadpost = i;
            replies.Reply = reply;
            replies.PostedDate = DateTime.Now;
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == currentUser);
            replies.PostedBy = user.NickName;
            context.Replies.Add(replies);
            try { 
            context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                              validationErrors.Entry.Entity.GetType().FullName,
                              validationError.PropertyName,
                              validationError.ErrorMessage);
                    }
                }
            }
            return RedirectToRoute("Test", new { Id = test });
        }

        public ActionResult ShowReplies(int id)
        {
            var test = context.Replies.Where(u => u.Threadpost.Id == id);
            return PartialView("_ShowReplies", test);
        }


        public ActionResult SearchForum(string searchstring)
        {
            var i = context.Thread.Where(u => u.Headline.Contains(searchstring));
            return PartialView("_PartialForum",i);
        }
    }
}