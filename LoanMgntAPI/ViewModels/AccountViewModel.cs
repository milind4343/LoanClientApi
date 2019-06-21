using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models;

namespace LoanMgntAPI.ViewModels
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponseModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("statuscode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class RegistrationVM
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        public string Username { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("roleid")]
        public int RoleId { get; set; }

        public string Password { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("isactive")]
        public bool? IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string ProfileImage { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        public string Phone { get; set; }

        [JsonProperty("gender")]

        public string Gender { get; set; }
        [JsonProperty("dob")]

        public DateTime? Dob { get; set; }

        [JsonProperty("walletamount")]
        public decimal WalletAmount { get; set; }
        
        //public string Dob { get; set; }
        [JsonProperty("profession")]
        public string Profession { get; set; }

        [JsonProperty("company")]
        public string CompanyName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("cityId")]
        public int? CityId { get; set; }

        [JsonProperty("stateId")]
        public int? StateId { get; set; }

        public int? Zipcode { get; set; }
    }

    public class FundVMList
    {
        public string AgentName { get; set; }
        public List<FundVM> lstFundVM { get; set; }
    }

    public class FundVM
    {

        [JsonProperty("agentfundid")]
        public Int32 AgentFundID { get; set; }
        [JsonProperty("agentid")]
        public Int32 AgentId { get; set; }
        [JsonProperty("fundamount")]
        public long FundAmount { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
        [JsonProperty("createddate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("depositby")]
        public string DepositBy { get; set; }
        [JsonProperty("agnetname")]
        public string AgnetName { get; set; }
        [JsonProperty("isreceive")]
        public bool IsReceive { get; set; }
    }

    public class LoanTypeVM
    {
        [JsonProperty("loantypeid")]
        public int LoanTypeId { get; set; }
        [JsonProperty("loantype")]
        public string LoanType { get; set; }
    }

   

    public class LoadDetailVM
    {
        [JsonProperty("customerid")]
        public Int32 CustomerID { get; set; }

        [JsonProperty("enddate")]
        public DateTime Enddate { get; set; }

        [JsonProperty("startdate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("interest")]
        public Int32 Interest { get; set; }

        [JsonProperty("interestamount")]
        public double InterestAmount { get; set; }

        [JsonProperty("interestpayat")]
        public string InterestPayAt { get; set; }

        [JsonProperty("loanamount")]
        public decimal LoanAmount { get; set; }

        [JsonProperty("loantypeid")]
        public string LoantypeId { get; set; }

        [JsonProperty("paymentamount")]
        public string PaymentAmount { get; set; }

        [JsonProperty("paymentperiodicity")]
        public string PaymentPeriodicity { get; set; }

        [JsonProperty("totalinstallments")]
        public Int32 TotalInstallments { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("tenure")]
        public List<TenureVM> lsttenure { get; set; }
    }

    public class TenureVM
    {
        [JsonProperty("installmentamount")]
        public double InstallmentAmount { get; set; }

        [JsonProperty("installmentdate")]
        public DateTime InstallmentDate { get; set; }
    }
}
