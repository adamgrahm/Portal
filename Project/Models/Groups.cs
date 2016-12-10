using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Groups
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Groupname is too long", MinimumLength = 3)]
        public string Groupname { get; set; }

        [Required]
        public ApplicationUser Creator { get; set; }

        public List<ApplicationUser> UsersInGroups { get; set; }



    }
}