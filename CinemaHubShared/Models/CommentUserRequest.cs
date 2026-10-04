using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class CommentUserRequest
    {
        public int? UserId { get; set; } = null;
        public int? FilmId { get; set; } = null;

        public string? CommentDescription { get; set; } = string.Empty;
        public DateTime? CommentDate { get; set; } = null;


    }
}
