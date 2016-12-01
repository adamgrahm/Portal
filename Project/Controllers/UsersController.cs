using Project.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult SaveUser(string username, [Bind(Include = "NickName, Info, DateOfBirth")] ApplicationUser user)
        {
            var i = context.Users.FirstOrDefault(u => u.UserName == username);
            user.UserName = username;
            i = user;
            context.SaveChanges();
            return PartialView("_PartialUsers", i);
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
    }
}