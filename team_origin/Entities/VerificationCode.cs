using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Code { get; set; }
    }
}
