using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class CustomerLoanDocumentDtl
    {
        public int DocumentId { get; set; }
        public int CustomerLoanId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FileName { get; set; }
        public DateTime? UploadedDate { get; set; }
        public int? UploadedDateInt { get; set; }
        public int? UploadedBy { get; set; }

        public CustomerLoan CustomerLoan { get; set; }
        public DocumentTypeMaster DocumentType { get; set; }
    }
}
