using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var users = context.Users.ToList();
            return View(users);
        }


        public ActionResult EditUser(string username)
        {
            var i = context.Users.Single(u => u.UserName == username);
            if (i.UserName == username)
            {
                return PartialView("_EditUser", i);
            }
            return View();
        }

        //FUNKAR EJ!!!!!!!!!
        [HttpPost]
        public ActionResult EditUser(string username, string firstname, string lastname, string email,
            string country, string city, string image, string info)
        {
            var i = context.Users.FirstOrDefault(u => u.UserName == username);
            i.FirstName = firstname;
            i.LastName = lastname;
            i.Email = email;
            i.Country = country;
            i.City = city;
            i.ImageURL = image;
            i.Info = info;
            context.SaveChanges();
            return RedirectToAction("Index", i);
        }

        public ActionResult DeleteUser(string username)
        {
            var i = context.Users.FirstOrDefault(u => u.UserName == username);
            if (i.UserName == username)
            {
                context.Users.Remove(i);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchstring)
        {
            var i = context.Users.Where(u => u.NickName.Contains(searchstring));
            return PartialView("_PartialUsers", i);
        }

        public ActionResult FindUser (string username)
        {
            var i = context.Users.FirstOrDefault(u => u.UserName == username);
            return PartialView("_DetailedUser", i);
        }

        public ActionResult DirectToUser(string username, int id)
        {
            var i = context.Users.FirstOrDefault(u => u.NickName == username);
            if (i == null)
            {
                var test = context.ThreadPost.Where(u => u.Id == id).Select(y => y.Thread.Id).FirstOrDefault();
                TempData["ErrorMessage"] = "User does not exist";
                
                return RedirectToRoute("Test", new { Id = test });                
            }
            return View(i);
        }

        public ActionResult DirectFromReply (string username, int id)
        {
            var i = context.Users.FirstOrDefault(u => u.NickName == username);
            var o = context.Replies.Where(x => x.Id == id).Select(t => t.Threadpost.Id).FirstOrDefault();
            if (i == null)
            {
                var test = context.ThreadPost.Where(u => u.Id == o).Select(y => y.Thread.Id).FirstOrDefault();
                TempData["ErrorMessage"] = "User does not exist";

                return RedirectToRoute("Test", new { Id = test });
            }

            return View("DirectToUser",i);
        }

        [Authorize]
        public ActionResult SearchForUser(string username)
        {
            var i = context.Users.Where(x => x.UserName.Contains(username));
            ViewBag.Message = username;
            return View(i);
        }

        [Authorize]
        public ActionResult DirectSearch(string username)
        {
            var i = context.Users.FirstOrDefault(x => x.UserName == username);
            return View("DirectToUser", i);
        }
    }
}