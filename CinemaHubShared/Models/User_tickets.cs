using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class User_tickets
    {
        public int Id { get; set; }
        public int? FK_User { get; set; }
        public int? FK_Ticket { get; set; }
    }
}
