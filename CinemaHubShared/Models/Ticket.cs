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
        public int? TypeTicketId { get; set; }
        public int? ZalId { get; set; }
        public int? TicketStatusId { get; set; }
        public DateTime? DateSold { get; set; }
        public int? SeansId { get; set; }
    }
}
