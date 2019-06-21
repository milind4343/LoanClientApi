using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class TicketMaster
    {
        public TicketMaster()
        {
            TicketDetail = new HashSet<TicketDetail>();
        }

        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool IsClose { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<TicketDetail> TicketDetail { get; set; }
    }
}
