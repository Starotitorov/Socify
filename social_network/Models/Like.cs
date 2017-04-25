using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class Like
    {
        public int Id { get; set; }

        public string VoterId { get; set; }

        public int PostId { get; set; }

        public virtual ApplicationUser Voter { get; set; }

        public virtual Post Post { get; set; }

        public DateTime? VotingTime { get; set; }
    }
}