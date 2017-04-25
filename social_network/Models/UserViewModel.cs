using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public string HomeTown { get; set; }

        public string FullName
        {
            get
            {
                if (FirstName != null && LastName != null) {
                    return $"{FirstName} {LastName}";
                }
                else
                {
                    return UserName;
                }
            }
        }

        public IEnumerable<PostViewModel> PostModels { get; set; }
    }
}