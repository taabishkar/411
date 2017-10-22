using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities
{
    public class FriendshipStatus
    {
        public int StatusId { get; set; }
        public string StatusDescription { get; set; }

        public ICollection<Friendship> Friendship{ get; set;}
    }
}
