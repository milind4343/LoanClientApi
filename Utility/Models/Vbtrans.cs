using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class Vbtrans
    {
        public long VbtransId { get; set; }
        public int AgentId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public int? PaymentMethodId { get; set; }
        public byte[] ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int? CreatedBy { get; set; }

        public LoginCredentials Agent { get; set; }
    }
}
