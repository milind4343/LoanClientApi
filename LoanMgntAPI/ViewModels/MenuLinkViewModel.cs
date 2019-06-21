﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanMgntAPI.ViewModels
{
    public class MenuLinkViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("iconname")]
        public string IconName { get; set; }

        [JsonProperty("routelink")]
        public string RouteLink { get; set; }

        [JsonProperty("home")]
        public bool Home { get; set; }

        [JsonProperty("children")]
        public List<MenuLinkViewModel> Children { get; set; }

        [JsonProperty("isview")]
        public bool IsView { get; set; }

        [JsonProperty("isadd")]
        public bool IsAdd { get; set; }

        [JsonProperty("isedit")]
        public bool IsEdit { get; set; }

        [JsonProperty("isdelete")]
        public bool IsDelete { get; set; }

        [JsonProperty("ischangestatus")]
        public bool IsChangeStatus { get; set; }

        [JsonProperty("moduleId")]
        public long ModuleId { get; set; }
    }
}
