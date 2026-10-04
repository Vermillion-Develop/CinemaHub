using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Cost { get; set; }
        public int? FK_Type_ticket { get; set; }
        public int? FK_Zal { get; set; }
        public int? FK_Ticket_status { get; set; }
        public DateTime? DateSold { get; set; }
        public int? FK_Seans { get; set; }
    }
}
