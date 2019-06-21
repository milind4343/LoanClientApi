using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class TicketDetail
    {
        public int TicketDtlId { get; set; }
        public int TicketId { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public TicketMaster Ticket { get; set; }
    }
}
