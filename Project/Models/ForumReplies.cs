using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ForumReplies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Reply")]
        [StringLength(5000, ErrorMessage = "Reply cannot be more than 5000 letters")]
        public string Reply { get; set; }

        [Required]
        public ThreadPost Threadpost { get; set; }


        public DateTime PostedDate { get; set; }

        public string PostedBy { get; set; }

        //public Thread Thread { get; set; }


    }
}