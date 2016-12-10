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
    public class GroupController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Group
        public ActionResult Index()
        {
            var currUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id == currUser);
            var groups = context.Groups.Where(x => x.Creator.Id == user.Id);
            if (groups != null)
            {
                return View(groups);
            }
            return View();
        }

        public ActionResult ReturnIndex()
        {
            var currUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id == currUser);
            var groups = context.Groups.Where(x => x.Creator.Id == user.Id);
            if (groups != null)
            {
                return PartialView("_PartialGroups", groups);
            }
            return PartialView("_PartialGroups");
            
        }

        public ActionResult ReturnGroupForm()
        {
            return PartialView("_CreateGroup");
        }

        public ActionResult CreateGroup(string groupname)
        {
            var currUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id == currUser);
            Groups group = new Groups();
            group.Creator = user;
            group.Groupname = groupname;
            context.Groups.Add(group);
            context.SaveChanges();
            var groups = context.Groups.Where(x => x.Creator.Id == user.Id);
            return PartialView("_PartialGroups", groups);
        }

        public ActionResult DeleteGroup(string groupname)
        {
            var findGroup = context.Groups.FirstOrDefault(x => x.Groupname == groupname);
            context.Groups.Remove(findGroup);
            var i = context.Groups.ToList();
            return View(i);
        }
    }
}