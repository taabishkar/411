using Microsoft.EntityFrameworkCore;
using System.Linq;
using team_origin.Entities;

namespace team_origin.Contracts
{
    public class FriendshipRespository : Repository<Friendship>, IFriendshipRepository
    {
        public FriendshipRespository(TeamOriginContext context) : base(context)
        {
        }

        //change return type to match needs.
        public bool AddFriend(string FromUserId, string ToUserId)
        {
            string friendshipcheck = CheckFriendship(FromUserId, ToUserId);
            if (friendshipcheck == "Not Friends")
            {
                //make sure friendstatus id is correct. Friendship status id shoould be the id for pending. 
                var friendship = new Friendship
                {
                    FromUserId = FromUserId,
                    ToUserId = ToUserId,
                    FriendshipStatusId = 0
                };

                _dbSet.Add(friendship);
                return true;
            }
            return false;
        }

        public string CheckFriendship(string FromUserId, string ToUserId)
        {
            var friendship = _dbSet.Include(b => b.FriendshipStatus)
                 .SingleOrDefault(f => f.FromUserId == FromUserId && f.ToUserId == ToUserId
                 || f.ToUserId == FromUserId && f.FromUserId == ToUserId);
            if (friendship == null)
            {
                return "Not Friends";
            }
            return friendship.FriendshipStatus.StatusDescription;
        }

        public Friendship AcceptRequest(string FromUserId, string ToUserId) {
            var friendship = _dbSet.Include(b => b.FriendshipStatus)
                 .SingleOrDefault(f => f.FromUserId == FromUserId && f.ToUserId == ToUserId
                 || f.ToUserId == FromUserId && f.FromUserId == ToUserId);

            return friendship;
        }
    }
}
