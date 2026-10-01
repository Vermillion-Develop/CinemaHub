using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Actor_in_filmDTO
    {
        public int Id { get; set; }
        public int? FK_Film { get; set; }
        public int? FK_Actor { get; set; }

        public byte[]? Actor_image { get; set; }
        public string? ActorFamily { get; set; }
        public string? ActorName { get; set; }
        public string? ActorFather { get; set; }
    }
}
