using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Rewards_in_filmDTO
    {
        public int Id { get; set; }
        public int? FK_Film { get; set; }
        public int? FK_Reward { get; set; }
        public DateOnly? Year { get; set; }


        public string? NameReward { get; set; }
        public byte[]? RewardImage { get; set; }

    }
}
