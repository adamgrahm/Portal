namespace Project.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Project.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Joined = DateTime.Now,
                    DateOfBirth = DateTime.Now,
                    Email = "Admin@Admin.Admin",
                    NickName = "Admin",
                    City = "Stockholm",
                    Country = "Sweden",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Info = "I am the administrator of this page",
                    ImageURL = "http://interworldgems.com/client_login/admin/images/login_icon.png"
                };
                manager.Create(user, "AdminIsAdmin");

                var rolestore = new RoleStore<IdentityRole>(context);
                var RoleManager = new RoleManager<IdentityRole>(rolestore);
                RoleManager.Create(new IdentityRole { Name = "Admin" });
                ApplicationUser findUser = manager.FindById(user.Id);
                manager.AddToRole(user.Id, "Admin");
            }

                if (!context.Users.Any(u => u.UserName == "Moderator"))
                {
                    var modstore = new UserStore<ApplicationUser>(context);
                    var modmanager = new UserManager<ApplicationUser>(modstore);
                var mod = new ApplicationUser
                {
                    UserName = "Moderator",
                    Joined = DateTime.Now,
                    DateOfBirth = DateTime.Now,
                    Email = "Mod@Mod.Mod",
                    NickName = "Moderator",
                    City = "Stockholm",
                    Country = "Sweden",
                    FirstName = "Moderator",
                    LastName = "Moderator",
                    Info = "I am the Moderator of this page",
                    ImageURL = "https://www.lwvlamv.org/wp-content/uploads/2014/01/IMAGE_the-moderator.png"
                };
                modmanager.Create(mod, "ModIsMod");

                    var modrolestore = new RoleStore<IdentityRole>(context);
                    var modRoleManager = new RoleManager<IdentityRole>(modrolestore);
                    modRoleManager.Create(new IdentityRole { Name = "Moderator" });
                    ApplicationUser findmod = modmanager.FindById(mod.Id);
                    modmanager.AddToRole(mod.Id, "Moderator");
                }
            }
        }
    }

