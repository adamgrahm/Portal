using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
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
            return View(context.Thread.ToList());
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
            return RedirectToAction("Index",context.ThreadPost.ToList());
        }

        public ActionResult ReplyToPost(int id, string reply)
        {
            ForumReplies replies = new ForumReplies();
            var i = context.Thread.FirstOrDefault(u => u.Id == id);
            //replies.Thread.Id = i.Id;
            replies.Reply = reply;
            context.Replies.Add(replies);
            context.SaveChanges();
            return RedirectToAction("Index",context.Replies.ToList());
        }
    }
}