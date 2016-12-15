using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{

    public class GroupController : Controller
    {
        //Fields that can be populated and sent back to the view
        private ApplicationUser user;
        private List<Groups> groups;
        private Groups group;
        //-------------------------------------

        // Returns all the groups that the logged in user are currently in, only for logged in users
        
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                groups = context.Groups.ToList();
                return View(groups);
                //if (User.Identity.IsAuthenticated)
                //{
                //var currUser = User.Identity.GetUserId();
                //user = context.Users.FirstOrDefault(x => x.Id == currUser);
                ////Find the groups where the user is in
                //groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                //if (groups != null)
                //{
                //    return View(groups);
                //}
                //return View();
                //}
                //else
                //{
                //    ViewBag.ErrorMessage = "You need to be logged in to access groups";
                //    return View();
                //}
            }
        }

        //Does the same thing as Index, but returns a partialview instead, which is useful for AJAX
        //Only for logged in users
        [Authorize]
        public ActionResult ReturnIndex()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                if (groups != null)
                {
                    return PartialView("_PartialGroups", groups);
                }
                return PartialView("_PartialGroups");
            }
        }

        //Returns the form where user can create a group
        //Only for loggged in users
        [Authorize]
        public ActionResult ReturnGroupForm()
        {
            return PartialView("_CreateGroup");
        }

        //Creates the new group and saves it to the database
        //Only for logged in users
        //Needs more logic for checking if groupname aldready exist to prevent duplicate groupnames
        [Authorize]
        public ActionResult CreateGroup(string groupname)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                Groups newGroup = new Groups();
                newGroup.Creator = user;
                newGroup.Groupname = groupname;
                if (newGroup.UsersInGroups == null)
                {
                    newGroup.UsersInGroups = new List<ApplicationUser>();
                }
                newGroup.UsersInGroups.Add(user);
                
                if (ModelState.IsValid)
                {
                    context.Groups.Add(newGroup);
                    context.SaveChanges();
                }
                
                groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                return RedirectToAction("Index");
            }
        }

        //Removes the group from the database
        //NOT TESTED!!
        public ActionResult DeleteGroup(string groupname)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                group = context.Groups.FirstOrDefault(x => x.Groupname == groupname);
                context.Groups.Remove(group);
                context.SaveChanges();
                groups = context.Groups.ToList();
                return View(groups);
            }
        }

        public ActionResult JoinGroup(string groupname)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                group = context.Groups.FirstOrDefault(x => x.Groupname == groupname);
                if (group.UsersInGroups == null)
                {
                    group.UsersInGroups = new List<ApplicationUser>();
                }
                group.UsersInGroups.Add(user);
                context.SaveChanges();
                return PartialView("_CreateGroup");
            }
        }
        //public ActionResult SelectedGroup(string groupname)
        //{
        //    using (ApplicationDbContext context = new ApplicationDbContext())
        //    {
        //        group = context.Groups.FirstOrDefault(x => x.Groupname == groupname);
        //        return View(group);
        //    }
        //}
    }
}