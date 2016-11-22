﻿using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            var blogs = context.Blogposts.ToList();
            return View(blogs);
        }

        public ActionResult NewBlogPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BlogPosted(string post, string headline)
        {
            var blogpost = new Blog();
            blogpost.Post = post;
            blogpost.Headline = headline;
            context.Blogposts.Add(blogpost);
            context.SaveChanges();
            return RedirectToAction("Index", context.Blogposts.ToList());
        }
    }
}