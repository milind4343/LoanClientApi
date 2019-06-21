using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class Link
    {
        public Link()
        {
            RoleRight = new HashSet<RoleRight>();
        }

        public int LinkId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string RouteLink { get; set; }
        public int ViewIndex { get; set; }
        public int ParentId { get; set; }
        public bool IsSinglePage { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }

        public ModuleMaster Module { get; set; }
        public ICollection<RoleRight> RoleRight { get; set; }
    }
}
