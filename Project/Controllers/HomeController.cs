using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {

       private List<Blog> blogs;
                                           
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                blogs = context.Blogposts.OrderByDescending(x => x.Id).Take(1).ToList();
                return View(blogs);
            }
        }
    }
}