using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string ContentType { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}