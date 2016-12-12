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

        [Required]
        [Display(Name = "Headline")]
        [StringLength(100, ErrorMessage = "Headline can not be more than 100 letters")]
        public string Headline { get; set; }

        public ApplicationUser OriginalPoster { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public string PostedBy { get; set; }

    }
    }
