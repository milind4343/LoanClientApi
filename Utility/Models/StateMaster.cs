using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class StateMaster
    {
        public StateMaster()
        {
            LoginCredentials = new HashSet<LoginCredentials>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<LoginCredentials> LoginCredentials { get; set; }
    }
}
