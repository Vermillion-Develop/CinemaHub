using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHubShared.Models
{
    public class Mesta_in_zal
    {
        public int Id { get; set; }
        public int? FK_Mesto { get; set; }
        public int? FK_Zal { get; set; }
        public int? FK_Status_mesto { get; set; }
    }
}
