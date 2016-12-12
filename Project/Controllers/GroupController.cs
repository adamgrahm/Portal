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
        //ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationUser user;
        private List<Groups> groups;
        private Groups group;
        // GET: Group
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currUser = User.Identity.GetUserId();
                user = context.Users.FirstOrDefault(x => x.Id == currUser);
                groups = context.Groups.Where(x => x.Creator.Id == user.Id).ToList();
                if (groups != null)
                {
                    return View(groups);
                }
                return View();
            }
        }

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

        public ActionResult ReturnGroupForm()
        {
            return PartialView("_CreateGroup");
        }

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