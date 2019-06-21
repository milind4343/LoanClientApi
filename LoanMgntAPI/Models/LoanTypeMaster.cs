using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class LoanTypeMaster
    {
        public LoanTypeMaster()
        {
            CustomerLoan = new HashSet<CustomerLoan>();
        }

        public int LoanTypeId { get; set; }
        public int? LoanType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<CustomerLoan> CustomerLoan { get; set; }
    }
}
