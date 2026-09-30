using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string? Father { get; set; } = string.Empty;
    }
}
