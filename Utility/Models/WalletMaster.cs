using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class WalletMaster
    {
        public WalletMaster()
        {
            WalletTransaction = new HashSet<WalletTransaction>();
        }

        public int WalletId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<WalletTransaction> WalletTransaction { get; set; }
    }
}
