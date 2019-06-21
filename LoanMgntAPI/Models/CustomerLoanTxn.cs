using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class CustomerLoanTxn
    {
        public int TransactionId { get; set; }
        public int CustomerLoanId { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime InstallmentDate { get; set; }
        public int? InstallmentDateInt { get; set; }
        public bool IsPaid { get; set; }
        public decimal PaneltyAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? PaidDateInt { get; set; }
        public string ReceiptNo { get; set; }
        public bool IsPrePay { get; set; }
        public bool IsLatePay { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public byte PaymentMethodId { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int? CreatedBy { get; set; }

        public CustomerLoan CustomerLoan { get; set; }
    }
}
