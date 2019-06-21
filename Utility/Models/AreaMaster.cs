using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class AreaMaster
    {
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public string AreaName { get; set; }
        public int? ZipCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public int CreatedBy { get; set; }
    }
}
