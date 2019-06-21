using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class EmailTemplate
    {
        public long EmailTemplateId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
    }
}
