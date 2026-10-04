using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class User_ticketsDTO
    {
        public int Id { get; set; }
        public int? FK_User { get; set; }
        public int? FK_Ticket { get; set; }

        public string? Family { get; set; }
        public string? Name { get; set; }
        public string? Father { get; set; }
        public string? TypeBilet { get; set; }
        public decimal? CostBilet { get; set; }
        public DateTime? DateBiletBuyed { get; set; }
        public string? NameBilet { get; set; }
        public string? TicketZal { get; set; }
        public string? BiletStatus { get; set; }

        
    }
}
