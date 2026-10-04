using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class BuyTicketRequest
    {
        
        public string? NameTicket { get; set; } = null;
        public decimal? CostTicket { get; set; } = null;
        public int? FK_Type_ticket { get; set; } = null;
        public int? FK_Zal { get; set; } = null;
        public int? FK_Ticket_status { get; set; } = null;
        public DateTime? DateSold { get; set; } = null;
        public int? FK_Seans { get; set; } = null;


        public int? UserId { get; set; } = null;
        public int? Mesto { get; set; } = null;
        
    }
}
