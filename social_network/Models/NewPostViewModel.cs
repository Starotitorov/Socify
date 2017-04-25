using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class NewPostViewModel
    {
        public HttpPostedFileBase Photo { get; set; }

        public string Body { get; set; }
    }
}