using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace team_origin.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VerificationCode VerificationCode { get; set; }
        public ICollection<Friendship> ToUserFriendship { get; set; }
        public ICollection<Friendship> FromUserFriendship { get; set; }
        public Mood Mood { get; set; }

    }
}
