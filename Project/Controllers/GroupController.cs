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
        [Authorize]
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                //Find the groups where the user is in
                groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                if (groups != null)
                {
                    return View(groups);
                }
                return View();
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
                Groups group = new Groups();
                group.Creator = user;
                group.Groupname = groupname;
                
                if (ModelState.IsValid)
                {
                    context.Groups.Add(group);
                    context.SaveChanges();
                }
                
                groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                return PartialView("_PartialGroups", groups);
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
    }
}