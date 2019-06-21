﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanMgntAPI.ViewModels
{
    public class RoleRightsViewModel
    {
        [JsonProperty("roleid")]
        public long RoleId { get; set; }

        [JsonProperty("modulename")]
        public string ModuleName { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("linkid")]
        public long LinkId { get; set; }

        [JsonProperty("isview")]
        public bool IsView { get; set; }

        [JsonProperty("isadd")]
        public bool IsAdd { get; set; }

        [JsonProperty("isedit")]
        public bool IsEdit { get; set; }

        [JsonProperty("isdelete")]
        public bool IsDelete { get; set; }

        [JsonProperty("havefullaccess")]
        public bool HaveFullAccess { get; set; }

        [JsonProperty("ischangestatus")]
        public bool IsChangeStatus { get; set; }

    }
}
