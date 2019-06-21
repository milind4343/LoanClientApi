using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class ModuleMaster
    {
        public ModuleMaster()
        {
            Link = new HashSet<Link>();
        }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public int? ViewIndex { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }

        public ICollection<Link> Link { get; set; }
    }
}
