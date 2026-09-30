using CinemaHubShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHub.Services
{
    public static class UserSession
    {
        public static CurrentUser? Current { get; set; }
    }
}
