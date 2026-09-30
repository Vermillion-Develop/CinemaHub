using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Janrs_in_film
    {
        public int Id { get; set; }
        public int? FilmId { get; set; }
        public int? JanrId { get; set; }
    }
}
