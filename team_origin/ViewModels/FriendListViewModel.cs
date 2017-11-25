using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.ViewModels
{
    public class FriendListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Phone { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
