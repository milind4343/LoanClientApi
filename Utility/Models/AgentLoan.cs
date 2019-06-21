using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class AgentLoan
    {
        public AgentLoan()
        {
            AgentLoanTxn = new HashSet<AgentLoanTxn>();
        }

        public int AgentLoanId { get; set; }
        public int AgentId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Interest { get; set; }
        public int Duration { get; set; }
        public DateTime InitiateDate { get; set; }
        public decimal? DueAmount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public LoginCredentials Agent { get; set; }
        public ICollection<AgentLoanTxn> AgentLoanTxn { get; set; }
    }
}
