using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Comments_in_filmDTO
    {
        public int Id { get; set; }
        public int? FK_Film { get; set; }
        public int? FK_Comment { get; set; }
        public int? FK_User { get; set; }

        public string? UserFamily { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public string? CommentDescription { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
