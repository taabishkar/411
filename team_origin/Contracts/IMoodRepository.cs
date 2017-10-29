using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities;

namespace team_origin.Contracts
{
    interface IMoodRepository
    {
        Mood GetMoodByUser(string UserId);
        bool UpdateMood(Mood Mood);
        bool DeleteMood(string UserId);
    }
}
