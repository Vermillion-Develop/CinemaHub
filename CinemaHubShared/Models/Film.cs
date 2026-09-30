using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Year { get; set; }
        public int? FK_Country_Version { get; set; }
        public int? FK_Adult_Rating { get; set; }
        public decimal? Rating { get; set; }
        public byte[]? Image { get; set; }
    }
}
