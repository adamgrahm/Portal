using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ThreadPost
    {
        [Key]
        public int Id { get; set; }

        public string ForumPost { get; set; }

        public Thread Thread { get; set; }

        public ApplicationUser OriginalPoster { get; set; }

        public DateTime DatePosted { get; set; }

    }
}