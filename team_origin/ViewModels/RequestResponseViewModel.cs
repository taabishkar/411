using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.ViewModels
{
    public class RequestResponseViewModel
    {
        public Notification Notification { get; set; }
        public string LoggedInUserId { get; set; }
    }
}
