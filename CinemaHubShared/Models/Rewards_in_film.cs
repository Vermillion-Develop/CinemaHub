using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Rewards_in_film
    {
        public int Id { get; set; }
        public int? FilmId { get; set; }
        public int? RewardId { get; set; }
        public DateOnly? Year { get; set; }

    }
}
