using Microsoft.AspNetCore.Identity;

namespace team_origin.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VerificationCode VerificationCode { get; set; }

    }
}
