using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class RoleRight
    {
        public int RoleRightId { get; set; }
        public int RoleId { get; set; }
        public int LinkId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsChangeStatus { get; set; }
        public bool IsView { get; set; }

        public Link Link { get; set; }
        public RoleMaster Role { get; set; }
    }
}
