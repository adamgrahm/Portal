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

        [Required]
        [Display(Name = "Forumpost")]
        [StringLength(5000, ErrorMessage = "Forumpost can not be more than 5000 letters", MinimumLength = 5)]
        public string ForumPost { get; set; }

        [Required]
        public Thread Thread { get; set; }

        public ApplicationUser OriginalPoster { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public string PostedBy { get; set; }

        public ForumReplies Replies { get; set; }



    }
}