using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Login
    {
        [Key]
        public string? Login_Id { get; set; }
        public string? Password { get; set; }
    }
}
