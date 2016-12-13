using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{

    public class BlogController : Controller
    {
        //Fields to populate and send back to the view
        private List<Blog> blogs;
        private string user;
        
        //Gets all blogposts from the database and send them to the view 
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                blogs = context.Blogposts.ToList();
                return View(blogs);
            }
        }

        //Saves a new filmreview in the database, only for Admins
        [Authorize(Roles = "Admin")]
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

        //Ability to search for a specific filmreview
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