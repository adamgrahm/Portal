using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public string Headline { get; set; }

        public string Post { get; set;}

        public ApplicationUser WhoPosted { get; set; }

        public List<Blog> ListOfBlog { get; set; }

    }
}