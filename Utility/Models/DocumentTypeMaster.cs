using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class DocumentTypeMaster
    {
        public DocumentTypeMaster()
        {
            CustomerLoanDocumentDtl = new HashSet<CustomerLoanDocumentDtl>();
        }

        public int DocumentTypeId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<CustomerLoanDocumentDtl> CustomerLoanDocumentDtl { get; set; }
    }
}
