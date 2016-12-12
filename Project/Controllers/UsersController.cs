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
        private List<ApplicationUser> getAllUsers;
        private ApplicationUser user;
        private int currentThread;

        // GET: Users
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.ToList();
                return View(getAllUsers);
            }
        }


        public ActionResult EditUser(string username)
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.Single(u => u.UserName == username);
                if (user.UserName == username)
                {
                    return PartialView("_EditUser", user);
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditUser(string username, string firstname, string lastname, string email,
            string country, string city, string image, string info)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username);
                user.FirstName = firstname;
                user.LastName = lastname;
                user.Email = email;
                user.Country = country;
                user.City = city;
                user.ImageURL = image;
                user.Info = info;
                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                }

                return RedirectToAction("Index", user);
            }
        }



        public ActionResult Search(string searchstring)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.Where(u => u.NickName.Contains(searchstring)).ToList();
                return PartialView("_PartialUsers", getAllUsers);
            }
        }

        public ActionResult FindUser(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username);
                return PartialView("_DetailedUser", user);
            }
        }

        public ActionResult DirectToUser(string username, int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.NickName == username);
                if (user == null)
                {
                    currentThread = context.ThreadPost.Where(u => u.Id == id).Select(y => y.Thread.Id).FirstOrDefault();
                    TempData["ErrorMessage"] = "User does not exist";

                    return RedirectToRoute("Test", new { Id = currentThread });
                }
                return View(user);
            }
        }

        public ActionResult DirectFromReply(string username, int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.NickName == username);
                currentThread = context.Replies.Where(x => x.Id == id).Select(t => t.Threadpost.Id).FirstOrDefault();
                if (user == null)
                {
                    var test = context.ThreadPost.Where(u => u.Id == currentThread).Select(y => y.Thread.Id).FirstOrDefault();
                    TempData["ErrorMessage"] = "User does not exist";

                    return RedirectToRoute("Test", new { Id = test });
                }

                return View("DirectToUser", user);
            }
        }

        [Authorize]
        public ActionResult SearchForUser(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.Where(x => x.UserName.Contains(username)).ToList();
                ViewBag.Message = username;
                return View(getAllUsers);
            }
        }

        [Authorize]
        public ActionResult DirectSearch(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.UserName == username);
                return View("DirectToUser", user);
            }
        }

        public ActionResult ShowUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.ToList();
                return View(getAllUsers);
            }
        }

        public ActionResult SelectedUser(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == username);
                return PartialView("_SelectedUser", user);
            }
        }
    }
}