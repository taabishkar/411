using team_origin.Entities;

namespace team_origin.Contracts
{
    public interface IFriendshipRepository
    {
        string CheckFriendship(string FromUserId, string ToUserId);

        bool AddFriend(string FromUserId, string ToUserId);

        Friendship AcceptRequest(string FromUserId, string ToUserId);
    }
}
