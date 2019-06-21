using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LoanMgntAPI.ViewModels
{
  public class CustomerViewModel
  {
    [JsonProperty("userID")]
    public int UserID { get; set; }

    [JsonProperty("firstname")]
    public string Firstname { get; set; }

    [JsonProperty("middlename")]
    public string Middlename { get; set; }

    [JsonProperty("lastname")]
    public string Lastname { get; set; }

    [JsonProperty("emailId")]
    public string EmailID { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("dob")]
    public DateTime DOB { get; set; }

    [JsonProperty("uid")]
    public string UID { get; set; }

    [JsonProperty("profession")]
    public string Profession { get; set; }

    [JsonProperty("company")]
    public string CompanyName { get; set; }

    [JsonProperty("stateId")]
    public int StateID { get; set; }

    [JsonProperty("cityId")]
    public int CityID { get; set; }

    [JsonProperty("zipcode")]
    public int? Zipcode { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("remarks")]
    public string Remarks { get; set; }

    //[JsonProperty("remarks")]
    public string ProfileImage { get; set; }

    public string ProfileImageURL { get; set; }

    //public string ProfileImageCode { get; set; }

    public bool IsActive { get; set; }
  }

  public class CustomerAddRequest
  {
    public string customer { get; set; }
    public IFormFile file { get; set; }
  }

  public class LoggedInUserVM
  {
    [JsonProperty("middlename")]
    public string Middlename { get; set; }

    [JsonProperty("lastname")]
    public string Lastname { get; set; }

    [JsonProperty("firstname")]
    public string Firstname { get; set; }

    public string ProfileImage { get; set; }

    public string ProfileImageURL { get; set; }

    [JsonProperty("roleId")]
    public int RoleID { get; set; }

  }

  public class CustomerLoanVM
  {
    [JsonProperty("customerLoanId")]
    public int CustomerLoanId { get; set; }

    [JsonProperty("agentid")]
    public int AgentId { get; set; }

    [JsonProperty("customerid")]
    public int CustomerId { get; set; }

    [JsonProperty("customername")]
    public string CustomerName { get; set; }

    [JsonProperty("loantypeid")]
    public int LoanTypeId { get; set; }

    [JsonProperty("loanAmount")]
    public decimal LoanAmount { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    [JsonProperty("interestamount")]
    public decimal? InterestAmount { get; set; }

    [JsonProperty("startdate")]
    public DateTime StartDate { get; set; }

    [JsonProperty("enddate")]
    public DateTime EndDate { get; set; }

    [JsonProperty("paymentperiodicity")]
    public string PaymentPeriodicity { get; set; }

    [JsonProperty("interestpayat")]
    public string InterestPayAt { get; set; }

    [JsonProperty("totalinstallments")]
    public byte TotalInstallments { get; set; }

    [JsonProperty("paymentamount")]
    public decimal PaymentAmount { get; set; }

    [JsonProperty("penaltyamount")]
    public decimal PenaltyAmount { get; set; }

    [JsonProperty("discountamount")]
    public decimal DiscountAmount { get; set; }

    [JsonProperty("ispaid")]
    public bool IsPaid { get; set; }

    [JsonProperty("remarks")]
    public string Remarks { get; set; }

    [JsonProperty("createddate")]
    public DateTime? CreatedDate { get; set; }

    [JsonProperty("createddateint")]
    public int? CreatedDateInt { get; set; }

    [JsonProperty("createdby")]
    public int? CreatedBy { get; set; }

    [JsonProperty("createdbyname")]
    public string CreatedByName { get; set; }
  }


  public class InstallmentsVM
  {
    [JsonProperty("transactionid")]
    public int TransactionId { get; set; }

    [JsonProperty("customerloanid")]
    public int CustomerLoanId { get; set; }

    [JsonProperty("installmentamount")]
    public decimal InstallmentAmount { get; set; }

    [JsonProperty("installmentdate")]
    public DateTime InstallmentDate { get; set; }

    [JsonProperty("installmentdateint")]
    public int? InstallmentDateInt { get; set; }

    [JsonProperty("ispaid")]
    public bool IsPaid { get; set; }

    [JsonProperty("paneltyamount")]
    public decimal PaneltyAmount { get; set; }

    [JsonProperty("discountamount")]
    public decimal DiscountAmount { get; set; }

    [JsonProperty("paiddate")]
    public DateTime? PaidDate { get; set; }

    [JsonProperty("paiddateint")]
    public int? PaidDateInt { get; set; }

    [JsonProperty("receiptno")]
    public string ReceiptNo { get; set; }

    [JsonProperty("isprepay")]
    public bool IsPrePay { get; set; }

    [JsonProperty("islatepay")]
    public bool IsLatePay { get; set; }

    [JsonProperty("receiptdate")]
    public DateTime? ReceiptDate { get; set; }

    [JsonProperty("paymentmethodid")]
    public byte PaymentMethodId { get; set; }

    [JsonProperty("chequeno")]
    public string ChequeNo { get; set; }

    [JsonProperty("bankname")]
    public string BankName { get; set; }

    [JsonProperty("createddate")]
    public DateTime? CreatedDate { get; set; }

    [JsonProperty("createddateint")]
    public int? CreatedDateInt { get; set; }
  }


  public class InstalmentTxnVM
  {
    [JsonProperty("installno")]
    public int InstallmentNo { get; set; }

    [JsonProperty("transactionid")]
    public int TransactionId { get; set; }

    [JsonProperty("customername")]
    public string CustomerName { get; set; }

    [JsonProperty("customerloanid")]
    public int CustomerLoanId { get; set; }

    [JsonProperty("loanamount")]
    public decimal LoanAmount { get; set; }

    [JsonProperty("installmentamount")]
    public decimal InstallmentAmount { get; set; }

    [JsonProperty("installmentdate")]
    public DateTime InstallmentDate { get; set; }

    [JsonProperty("installmentdateint")]
    public int? InstallmentDateInt { get; set; }

    [JsonProperty("ispaid")]
    public bool IsPaid { get; set; }

    [JsonProperty("paneltyamount")]
    public decimal PaneltyAmount { get; set; }

    [JsonProperty("discountamount")]
    public decimal DiscountAmount { get; set; }

    [JsonProperty("paidamount")]
    public decimal PaidAmount { get; set; }

    [JsonProperty("paiddate")]
    public DateTime? PaidDate { get; set; }

    [JsonProperty("paiddateint")]
    public int? PaidDateInt { get; set; }

    [JsonProperty("receiptno")]
    public string ReceiptNo { get; set; }

    [JsonProperty("isprepay")]
    public bool IsPrePay { get; set; }

    [JsonProperty("islatepay")]
    public bool IsLatePay { get; set; }

    [JsonProperty("receiptdate")]
    public DateTime? ReceiptDate { get; set; }

    [JsonProperty("paymentmethodid")]
    public byte PaymentMethodId { get; set; }

    [JsonProperty("chequeno")]
    public string ChequeNo { get; set; }

    [JsonProperty("bankname")]
    public string BankName { get; set; }

    [JsonProperty("createddate")]
    public DateTime? CreatedDate { get; set; }

    [JsonProperty("createddateint")]
    public int? CreatedDateInt { get; set; }

    [JsonProperty("createdby")]
    public int? CreatedBy { get; set; }

    [JsonProperty("remarks")]
    public string Remarks { get; set; }

  
  }

  public class LoanRequestVM
  {
    public List<UploadDoc> uploadDoc { get; set; }
    public string loandetail { get; set; }
  }

  public class UploadDoc
  {
    [JsonProperty("filedata")]
    public IFormFile filedata { get; set; }

    [JsonProperty("docType")]
    public int DocType { get; set; }

    [JsonProperty("isChecked")]
    public bool isChecked { get; set; }

    [JsonProperty("downloadPath")]
    public string DownloadPath { get; set; }

    [JsonProperty("filename")]
    public string Filename { get; set; }
  }

}
