using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class AgentBalanceMst
    {
        public long BalanceId { get; set; }
        public int AgentId { get; set; }
        public decimal Lb { get; set; }
        public decimal Vb { get; set; }
        public decimal Bb { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public LoginCredentials Agent { get; set; }
    }
}
