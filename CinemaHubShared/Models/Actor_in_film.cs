using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Actor_in_film
    {
        public int Id { get; set; }
        public int IdFilm { get; set; }
        public int IdActor { get; set; }
    }
}
