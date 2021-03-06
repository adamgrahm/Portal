﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Groups = new HashSet<Groups>();
        }
        public string Info { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NickName { get; set; }
        public string ImageURL { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime Joined { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public bool IsLockedOut { get; set; }
        public bool InRole { get; set; }





        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Blog> Blogposts { get; set; }
        public DbSet<Thread> Thread { get; set; }
        public DbSet<ThreadPost> ThreadPost { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<ForumReplies> Replies { get; set; }
        public DbSet<Conversations> Conversations { get; set; }
        public DbSet<Groups> Groups { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}