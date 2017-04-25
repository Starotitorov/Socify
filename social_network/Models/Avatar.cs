using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class Avatar
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string ContentType { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}