using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class SearchModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string Gender { get; set; }
        public string HomeTown { get; set; }
    }
}