using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.Entities
{
    public class Friendship
    {
        public int FrienshipId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int FriendshipStatusId { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}
