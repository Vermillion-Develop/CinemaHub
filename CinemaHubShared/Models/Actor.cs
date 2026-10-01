using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string? Family { get; set; }
        public string? Name { get; set; }
        public string? Father { get; set; }
        public int? FK_Gender { get; set; }
        public int? FK_Nation { get; set; }
        public DateOnly? DateBirthday { get; set; }
        public byte[]? Actor_image { get; set; }
    }
}
