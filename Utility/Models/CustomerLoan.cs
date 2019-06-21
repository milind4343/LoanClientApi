using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class CustomerLoan
    {
        public CustomerLoan()
        {
            CustomerLoanDocumentDtl = new HashSet<CustomerLoanDocumentDtl>();
            CustomerLoanTxn = new HashSet<CustomerLoanTxn>();
        }

        public int CustomerLoanId { get; set; }
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public int LoanTypeId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Interest { get; set; }
        public decimal? InterestAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentPeriodicity { get; set; }
        public string InterestPayAt { get; set; }
        public byte TotalInstallments { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PenaltyAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsPaid { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public LoginCredentials Agent { get; set; }
        public LoginCredentials CreatedByNavigation { get; set; }
        public LoginCredentials Customer { get; set; }
        public LoanTypeMaster LoanType { get; set; }
        public ICollection<CustomerLoanDocumentDtl> CustomerLoanDocumentDtl { get; set; }
        public ICollection<CustomerLoanTxn> CustomerLoanTxn { get; set; }
    }
}
