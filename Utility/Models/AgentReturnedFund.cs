using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class AgentReturnedFund
    {
        public int TransactionId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public byte? PaymentMethodId { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public decimal? ReturnAmount { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
    }
}
