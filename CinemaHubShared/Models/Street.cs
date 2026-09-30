using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? TownId { get; set; }
    }
}
