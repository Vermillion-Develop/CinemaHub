using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
