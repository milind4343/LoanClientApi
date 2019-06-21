using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            RoleRight = new HashSet<RoleRight>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<RoleRight> RoleRight { get; set; }
    }
}
