using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanMgntAPI.ViewModels
{
    public class RoleViewModel
    {
        [JsonProperty("roleid")]
        public long RoleId { get; set; }

        [JsonProperty("rolename")]
        public string RoleName { get; set; }

        [JsonProperty("isactive")]
        public bool IsActive { get; set; }
    }
}
