using System;
using System.Collections.Generic;

namespace Utility.Models
{
    public partial class LoginCredentials
    {
        public LoginCredentials()
        {
            AgentBalanceMst = new HashSet<AgentBalanceMst>();
            AgentFundAgent = new HashSet<AgentFund>();
            AgentFundCreatedByNavigation = new HashSet<AgentFund>();
            CustomerLoanAgent = new HashSet<CustomerLoan>();
            CustomerLoanCreatedByNavigation = new HashSet<CustomerLoan>();
            CustomerLoanCustomer = new HashSet<CustomerLoan>();
            Vbtrans = new HashSet<Vbtrans>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string ProfileImage { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Profession { get; set; }
        public string CompanyName { get; set; }
        public string Uid { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? Zipcode { get; set; }
        public decimal? WalletAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedDateInt { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedDateInt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Remarks { get; set; }

        public CityMaster City { get; set; }
        public StateMaster State { get; set; }
        public ICollection<AgentBalanceMst> AgentBalanceMst { get; set; }
        public ICollection<AgentFund> AgentFundAgent { get; set; }
        public ICollection<AgentFund> AgentFundCreatedByNavigation { get; set; }
        public ICollection<CustomerLoan> CustomerLoanAgent { get; set; }
        public ICollection<CustomerLoan> CustomerLoanCreatedByNavigation { get; set; }
        public ICollection<CustomerLoan> CustomerLoanCustomer { get; set; }
        public ICollection<Vbtrans> Vbtrans { get; set; }
    }
}
