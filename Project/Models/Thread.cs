using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }

        public string Headline { get; set; }

        public ApplicationUser OriginalPoster { get; set; }

        public DateTime DatePosted { get; set; }
    }
}