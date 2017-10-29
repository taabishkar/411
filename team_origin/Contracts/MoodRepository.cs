using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities;

namespace team_origin.Contracts
{
    public class MoodRepository : Repository<Mood>, IMoodRepository
    {
        public MoodRepository(TeamOriginContext context) : base(context)
        {
        }

        public bool DeleteMood(string UserId)
        {
            throw new NotImplementedException();
        }

        public Mood GetMoodByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMood(Mood Mood)
        {
            throw new NotImplementedException();
        }
    }
}
