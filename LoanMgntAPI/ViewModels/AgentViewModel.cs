using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanMgntAPI.ViewModels
{
    public class AgentViewModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Company { get; set; }
        public string StateId { get; set; }
    }
}
