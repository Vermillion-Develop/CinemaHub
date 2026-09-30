using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string? Family { get; set; }
        public string? Name { get; set; }
        public string? Father { get; set; }
        public int? FK_Role { get; set; }
        public string? FK_Login { get; set; }
        public DateTime? Date_registration { get; set; }
    }
}
