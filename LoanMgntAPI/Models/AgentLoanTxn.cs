using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class AgentLoanTxn
    {
        public int TransactionId { get; set; }
        public int LoanId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public byte? PaymentMethodId { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public AgentLoan Loan { get; set; }
    }
}
