using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class WalletTransaction
    {
        public int TransactinId { get; set; }
        public int WalletId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public WalletMaster Wallet { get; set; }
    }
}
