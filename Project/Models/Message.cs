﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Identifier { get; set; }

        //public string Headline { get; set; }

        public string Content { get; set; }

        public ApplicationUser SentFrom { get; set; }

        public ApplicationUser SentTo { get; set; }

        public DateTime DateSent { get; set; }

        public string ToUser { get; set; }

        public string FromUser { get; set; }

        //public Conversations Conversation { get; set; }








    }
}