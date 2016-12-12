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
    public class BlogController : Controller
    {
        //ApplicationDbContext context = new ApplicationDbContext();
        private List<Blog> blogs;
        private string user;
        // GET: Blog
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                blogs = context.Blogposts.ToList();
                return View(blogs);
            }
        }

        public ActionResult NewBlogPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BlogPosted(string post, string headline, string pic)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var blogpost = new Blog();
                blogpost.Post = post;
                blogpost.Headline = headline;
                user = User.Identity.GetUserName().ToString();
                blogpost.PostedBy = user;
                blogpost.ImageURL = pic;
                
                if (ModelState.IsValid)
                {
                    context.Blogposts.Add(blogpost);
                    context.SaveChanges();
                }
                blogs = context.Blogposts.ToList();
                return RedirectToAction("Index", blogs);
            }
        }

        public ActionResult Search(string moviename)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                blogs = context.Blogposts.Where(x => x.Headline.Contains(moviename)).ToList();
                return PartialView("_PartialBlogs", blogs);
            }
        }
    }
}