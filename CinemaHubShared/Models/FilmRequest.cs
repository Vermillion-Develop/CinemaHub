using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class FilmRequest
    {
        public DateOnly? DateFilm { get; set; } = null;
        public int? ZalId { get; set; } = null;
    }
}
