﻿using Microsoft.AspNet.Identity;
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
        
        private Thread thread;
        private List<Thread> threads;
        private ThreadPost threadPost;
        private List<ForumReplies> replies;
        private int currentThread;
        private ApplicationUser user;
        // GET: Forum
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                threads = context.Thread.ToList();
                return View(threads);
            }
        }

        [HttpPost]
        public ActionResult Index(string headline)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newThread = new Thread();
                newThread.DatePosted = DateTime.Now;
                newThread.Headline = headline;
                var currentUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(r => r.Id == currentUser);
                newThread.OriginalPoster = user;
                newThread.PostedBy = user.UserName;
                
                if (ModelState.IsValid)
                {
                    context.Thread.Add(newThread);
                    context.SaveChanges();
                }
                
                threads = context.Thread.ToList();
                return PartialView("_PartialForum", threads);
            }
        }

        public ActionResult SelectedPost(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                thread = context.Thread.Single(u => u.Id == id);
                if (thread.Id == id)
                {
                    if (TempData["ErrorMessage"] != null)
                    {
                        ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                    }
                    return View(thread);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult NewForumPost(string headline, string forumpost, int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newPost = new ThreadPost();
                newPost.DatePosted = DateTime.Now;
                newPost.ForumPost = forumpost;
                thread = context.Thread.FirstOrDefault(u => u.Id == id);
                newPost.Thread = thread;
                var currentUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(u => u.Id == currentUser);
                newPost.PostedBy = user.UserName;
                context.ThreadPost.Add(newPost);

                if (ModelState.IsValid)
                {
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }

                    
                }
                
                return RedirectToRoute("Test", new { Id = id });
            }
        }

        [HttpPost]
        public ActionResult ReplyToPost(string reply, int id)//<---ThreadpostId
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ForumReplies replies = new ForumReplies();
                currentThread = context.ThreadPost.Where(u => u.Id == id).Select(y => y.Thread.Id).FirstOrDefault();
                threadPost = context.ThreadPost.FirstOrDefault(u => u.Id == id);
                replies.Threadpost = threadPost;
                replies.Reply = reply;
                replies.PostedDate = DateTime.Now;
                var currentUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(u => u.Id == currentUser);
                replies.PostedBy = user.UserName;
                
                if (ModelState.IsValid)
                {
                    context.Replies.Add(replies);
                    context.SaveChanges();
                }
                
                return RedirectToRoute("Test", new { Id = currentThread });
            }
        }

        public ActionResult ShowReplies(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                replies = context.Replies.Where(u => u.Threadpost.Id == id).ToList();
                return PartialView("_ShowReplies", replies);
            }
        }

        [HttpPost]
        public ActionResult SearchForum(string searchstring)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                threads = context.Thread.Where(u => u.Headline.Contains(searchstring)).ToList(); ;
            return PartialView("_PartialForum", threads);
        }
        }
    }
}