using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class AgentFund
    {
        public long AgentFundId { get; set; }
        public int AgentId { get; set; }
        public decimal FundAmount { get; set; }
        public bool? IsReceive { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public LoginCredentials Agent { get; set; }
        public LoginCredentials CreatedByNavigation { get; set; }
    }
}
