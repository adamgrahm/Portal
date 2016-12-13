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
        //Fields that can be populated and sent back to the view
        private List<ApplicationUser> getAllUsers;
        private ApplicationUser user;
        private int currentThread;
        //---------------------------------------

        // Returns a list with all users in the database
        //Only for Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.ToList();
                return View(getAllUsers);
            }
        }

        //Returns a view with input fields in order to assign new values to a user
        //Only for Admins
        [Authorize(Roles = "Admin")]
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

        //Saves the new values for the user to the database
        //Only for Admins
        [Authorize(Roles = "Admin")]
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


        //Ability to search for a specific user
        //Only for Admins
        [Authorize(Roles = "Admin")]
        public ActionResult Search(string searchstring)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.Where(u => u.NickName.Contains(searchstring)).ToList();
                return PartialView("_PartialUsers", getAllUsers);
            }
        }

        //Shows a view of the selected user with all the values assigned to that account
        //Only for Admins
        [Authorize(Roles = "Admin")]
        public ActionResult FindUser(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username);
                if (user.LockoutEndDateUtc > DateTime.Now)
                {
                    user.IsLockedOut = true;
                }

                return PartialView("_DetailedUser", user);
                
            }
        }

        //Redirects from any part of the page to the selected users profile
        //Availible for anyone
        //Is used to redirect from the threadposts!!
        public ActionResult DirectToUser(string username, int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.NickName == username);
                if (user == null)
                {
                    //In case the user does not exist, an errormessage is created and sent to the view
                    currentThread = context.ThreadPost.Where(u => u.Id == id).Select(y => y.Thread.Id).FirstOrDefault();
                    TempData["ErrorMessage"] = "User does not exist";

                    return RedirectToRoute("Test", new { Id = currentThread });
                }
                return View(user);
            }
        }

        //Redirects from any part of the page to the selected users profile
        //Availible for anyone
        //Is used to redirect from the forumreplies!!
        public ActionResult DirectFromReply(string username, int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.NickName == username);
                currentThread = context.Replies.Where(x => x.Id == id).Select(t => t.Threadpost.Id).FirstOrDefault();
                if (user == null)
                {
                    //In case the user does not exist, an errormessage is created and sent to the view
                    var test = context.ThreadPost.Where(u => u.Id == currentThread).Select(y => y.Thread.Id).FirstOrDefault();
                    TempData["ErrorMessage"] = "User does not exist";

                    return RedirectToRoute("Test", new { Id = test });
                }

                return View("DirectToUser", user);
            }
        }

        //Ability to search for a specific user
        //Availible for anyone
        public ActionResult SearchForUser(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.Where(x => x.UserName.Contains(username)).ToList();
                ViewBag.Message = username;
                return View(getAllUsers);
            }
        }

        //Redirect to the selected users profile page
        //Availible for anyone
        public ActionResult DirectSearch(string username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.UserName == username);
                return View("DirectToUser", user);
            }
        }

        //Shows a list of all the users
        //Only for logged in users
        public ActionResult ShowUser()
        {
            if (User.Identity.IsAuthenticated)
            {

           
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                getAllUsers = context.Users.ToList();
                return View(getAllUsers);
            }
            }
            else
            {
                ViewBag.ErrorMessage = "You need to be logged in to access users";
                return View();
            }
        }

        //Toggle the different profiles, with AJAX
        //Only for logged in users
        [Authorize]
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