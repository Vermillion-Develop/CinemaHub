using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class RateUserRequest
    {
        
        public int? UserRate { get; set; } = null;
        public int? FilmId { get; set; } = null;
        public int? UserId { get; set; } = null;
    }
}
