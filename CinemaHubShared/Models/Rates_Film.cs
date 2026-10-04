using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Rates_Film
    {
        public int Id { get; set; }
        public int? FK_Film { get; set; }
        public int? FK_User { get; set; }
        public int? Rates { get; set; }
    }
}
