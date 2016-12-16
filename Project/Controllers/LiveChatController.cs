using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class LiveChatController : Controller
    {
        // GET: LiveChat
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            ViewBag.User = user;
            return View();
        }
    }
}