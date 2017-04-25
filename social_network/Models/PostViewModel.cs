using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string PublicationTime { get; set; }

        public string SenderId { get; set; }

        public string SenderFullName { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }
}