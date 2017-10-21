using Microsoft.EntityFrameworkCore;
using System.Linq;
using team_origin.Entities;

namespace team_origin.Contracts
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TeamOriginContext context) : base(context)
        {
        }

        public User GetUserWithVerificationCode(string UserName)
        {
            //Search the database with the UserName(from parameter)
            //Include any VerificationCode they might have.
            User user = _dbSet
               .Include(i => i.VerificationCode)
               .SingleOrDefault(u => u.UserName == UserName);
            return user;
        }
    }
}
