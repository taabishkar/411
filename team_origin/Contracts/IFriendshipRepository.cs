using System.Collections.Generic;
using team_origin.Entities;
using team_origin.Entities.Notifications;

namespace team_origin.Contracts
{
    public interface IFriendshipRepository
    {
        string CheckFriendship(string FromUserId, string ToUserId);

        bool AddFriend(string FromUserId, string ToUserId);

        Friendship AcceptRequest(string FromUserId, string ToUserId);
        List<User> GetAllFriendsByUserId(string UserId);
    }
}
