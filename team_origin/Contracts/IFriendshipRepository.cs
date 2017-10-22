namespace team_origin.Contracts
{
    public interface IFriendshipRepository
    {
        string CheckFriendship(string FromUserId, string ToUserId);

        bool AddFriend(string FromUserId, string ToUserId);
    }
}
