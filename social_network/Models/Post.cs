using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime PublicationTime { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }
    }
}