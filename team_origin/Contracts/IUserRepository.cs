using team_origin.Entities;

namespace team_origin.Contracts
{
    public interface IUserRepository
    {
        User GetUserWithVerificationCode(string UserName);
    }
}
