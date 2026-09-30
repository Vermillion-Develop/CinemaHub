using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class SeansDTO
    {
        public int Id { get; set; }
        public int? FK_Film { get; set; }
        public int? FK_Zal { get; set; }
        public DateTime? Date_start { get; set; }
        public DateTime? Date_end { get; set; }
        public DateOnly? Day { get; set; }

        public decimal? FilmRate { get; set; }
        public string? FilmName { get; set; }
        public string? FilmDescription { get; set; }
        public string? FilmAdultRate { get; set; }
        public string? FilmVersion { get; set; }
        public byte[]? FilmHashedImg { get; set; }

    }
}
