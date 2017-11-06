using team_origin.Entities;
using team_origin.Entities.Notifications;

namespace team_origin.Contracts
{
    public interface IUserRepository
    {
        User GetUserWithVerificationCode(string UserName);
    }
}
