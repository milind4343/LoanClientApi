using System;
using System.Collections.Generic;

namespace LoanMgntAPI.Models
{
    public partial class CityMaster
    {
        public CityMaster()
        {
            LoginCredentials = new HashSet<LoginCredentials>();
        }

        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }

        public CityMaster City { get; set; }
        public CityMaster InverseCity { get; set; }
        public ICollection<LoginCredentials> LoginCredentials { get; set; }
    }
}
