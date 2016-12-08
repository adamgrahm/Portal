using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Conversations
    {
        public int Id { get; set; }

        public ApplicationUser CurrUser { get; set; }

        public ApplicationUser Receiver { get; set; }

        //public Message Content { get; set; }

        public DateTime DateSent { get; set; }


    }
}