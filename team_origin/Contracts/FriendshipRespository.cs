using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using team_origin.Entities;
using team_origin.Entities.Notifications;

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
                //make sure friendstatus id is correct. Friendship status id should be the id for pending. 
                var friendship = new Friendship
                {
                    FromUserId = FromUserId,
                    ToUserId = ToUserId,
                    FriendshipStatusId = 1
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

        public List<User> GetAllFriendsByUserId(string UserId)
        {
            List<User> friends = null;
            try
            {                
                var friendIds = ((from f in _dbContext.Friendship
                                 join u in _dbContext.Users on f.FromUserId equals u.Id
                                 where 
                                 f.FromUserId == UserId && f.FriendshipStatusId == 2
                                 select f.ToUserId)
                                 .Union
                                (from f1 in _dbContext.Friendship
                                 join u in _dbContext.Users on f1.ToUserId equals u.Id
                                 where
                                      f1.ToUserId == UserId && f1.FriendshipStatusId == 2
                                 select f1.FromUserId)).ToList();

                if(friendIds != null)
                {
                    friends = (from u in _dbContext.Users where friendIds.Contains(u.Id) select u).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return friends;
        }
    }
}
