using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Utility.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LoanMgntAPI.Controllers
{
  [EnableCors("CORS")]
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly LoanManagementContext _context;
    private readonly IMapper _mapper;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IConfiguration _configuration;

    public CustomerController(LoanManagementContext loanManagementContext, IMapper mapper, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
    {
      _context = loanManagementContext;
      _mapper = mapper;
      _hostingEnvironment = hostingEnvironment;
      _configuration = configuration;
    }

    [HttpGet("list/{userId?}")]
    public IActionResult CustomerList(int userId = 0)
    {

      List<LoginCredentials> list = new List<LoginCredentials>();
      List<CustomerViewModel> custList = new List<CustomerViewModel>();

      if (userId == 0)
      {
        var currentUser = HttpContext.User;
        Int32 loggedinuserroleId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "role_id").Value);
        if (loggedinuserroleId == (Int32)Helper.EnumList.Roles.Agent)
        {
          Int32 loggedinuserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);
          list = _context.LoginCredentials.Where(q => q.RoleId == (Int32)Helper.EnumList.Roles.Customer && q.IsDelete == false && q.IsActive == true && q.CreatedBy == loggedinuserId).ToList();
          custList = _mapper.Map<List<CustomerViewModel>>(list);
        }
        else if (loggedinuserroleId == (Int32)Helper.EnumList.Roles.Admin)
        {
          list = _context.LoginCredentials.Where(q => q.RoleId == (Int32)Helper.EnumList.Roles.Customer && q.IsDelete == false && q.IsActive == true).ToList();
          custList = _mapper.Map<List<CustomerViewModel>>(list);
        }
      }
      else
      {
        list = _context.LoginCredentials.Where(q => q.UserId == userId && q.IsDelete == false).ToList();
        custList = _mapper.Map<List<CustomerViewModel>>(list);

        string folderName = "Images";
        string webRootPath = _hostingEnvironment.WebRootPath;
        string imagename = custList.FirstOrDefault().ProfileImage;
        var domain = _configuration["DevImageDomain"];

        if (imagename != null)
        {
          string uploadFilesPath = Path.Combine(webRootPath, folderName, imagename);
          if (System.IO.File.Exists(uploadFilesPath))
          {
            //byte[] imageBytes = System.IO.File.ReadAllBytes(uploadFilesPath);
            //custList.FirstOrDefault().ProfileImageCode = Convert.ToBase64String(imageBytes);
            custList.FirstOrDefault().ProfileImageURL = string.Format("{0}/Images/{1}", domain, imagename);
          }
        }
        else
        {
          string uploadFilesPath = Path.Combine(webRootPath, folderName, "user-placeholder.png");
          if (System.IO.File.Exists(uploadFilesPath))
          {
            byte[] imageBytes = System.IO.File.ReadAllBytes(uploadFilesPath);
            //custList.FirstOrDefault().ProfileImageCode = Convert.ToBase64String(imageBytes);
          }
        }
      }
      //var res = new
      //{
      //    data = customerlist;
      //};
      //return Json(res);
      return Ok(custList);
    }

    [HttpPost("add")]
    public async Task<IActionResult> CustomerRegistration([FromForm]CustomerAddRequest request)
    {
      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

      LoginCredentials objLoginCredentials = new LoginCredentials();
      string folderName = "Images";

      try
      {
        var customer = JsonConvert.DeserializeObject<CustomerViewModel>(request.customer);
        if (customer.UserID == 0)
        {
          objLoginCredentials = _mapper.Map<LoginCredentials>(customer);
          objLoginCredentials.IsActive = true;
          objLoginCredentials.IsDelete = false;
          objLoginCredentials.Username = customer.EmailID;
          objLoginCredentials.RoleId = (Int32)Helper.EnumList.Roles.Customer;
          objLoginCredentials.CreatedBy = userId;
          objLoginCredentials.CreatedDate = DateTime.Now;
          objLoginCredentials.Password = Helper.Helper.CreateRandomPassword();

          #region image upload

          //string webRootPath = _hostingEnvironment.WebRootPath;
          //string newPath = Path.Combine(webRootPath, folderName);
          //if (!Directory.Exists(newPath))
          //{
          //    Directory.CreateDirectory(newPath);
          //}
          //if (file.Length > 0)
          //{
          //    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          //    string fullPath = Path.Combine(newPath, fileName);
          //    using (var stream = new FileStream(fullPath, FileMode.Create))
          //    {
          //        file.CopyTo(stream);
          //    }
          //}

          if (request.file != null) //return BadRequest("Null File");
          {
            if (request.file.Length == 0)
            {
              return BadRequest("Empty File");
            }
            if (request.file.Length > 2 * 1024 * 1024) return BadRequest("Max file size exceeded.");
            string webRootPath = _hostingEnvironment.WebRootPath;
            string uploadFilesPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(uploadFilesPath))
            {
              Directory.CreateDirectory(uploadFilesPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.file.FileName);
            var filePath = Path.Combine(uploadFilesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
              await request.file.CopyToAsync(stream);
            }

            objLoginCredentials.ProfileImage = fileName;
          }

          #endregion

          _context.Add(objLoginCredentials);
          _context.SaveChanges();
        }
        else
        {
          var data = _context.LoginCredentials.FirstOrDefault(q => q.UserId == customer.UserID && q.IsActive == true && q.IsDelete == false);
          var result = _mapper.Map(customer, data);

          result.Username = customer.EmailID;

          #region remove existing profile picture

          if (request.file != null)
          {
            string webRootPath1 = _hostingEnvironment.WebRootPath;
            string imagename1 = data.ProfileImage;
            imagename1 = imagename1 == null ? Guid.NewGuid().ToString() + Path.GetExtension(request.file.FileName) : imagename1;
            string uploadFilesPath1 = Path.Combine(webRootPath1, folderName, imagename1);
            using (var stream = new FileStream(uploadFilesPath1, FileMode.Create))
            {
              await request.file.CopyToAsync(stream);
              result.ProfileImage = imagename1;
            }
          }

          #endregion

          _context.Entry(result).State = EntityState.Modified;
          _context.SaveChanges();
        }

        return Ok(new { success = true });
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
    }


    [HttpGet("changeStatus/{userId}")]
    public IActionResult ChangeActiveStatus(int userId)
    {
      try
      {
        LoginCredentials user = new LoginCredentials();
        if (userId > 0)
        {
          user = _context.LoginCredentials.Find(userId);
          user.IsActive = user.IsActive.Value == true ? false : true;
          _context.Entry(user).State = EntityState.Modified;
          _context.SaveChanges();
        }
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(new { success = true });
    }


    [HttpPost("assign")]
    public async Task<IActionResult> AssignLoan([FromForm]LoanRequestVM loanrequest)
    {
      List<CustomerLoanDocumentDtl> lstLoanDocument = new List<CustomerLoanDocumentDtl>();
      string folderName = "CustomerDoc";
      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

      try
      {
        CustomerLoan objCustomerLoan = new CustomerLoan();
        var loan = JsonConvert.DeserializeObject<LoadDetailVM>(loanrequest.loandetail);
        if (loan != null)
        {
          objCustomerLoan = _mapper.Map<CustomerLoan>(loan);
          objCustomerLoan.CreatedBy = userId;
          objCustomerLoan.AgentId = userId;

          objCustomerLoan.CreatedDate = DateTime.Now;
          _context.CustomerLoan.Add(objCustomerLoan);
          _context.SaveChanges();

          #region CustomerLoanTxn

          if (loan.lsttenure != null && loan.lsttenure.Count > 0)
          {
            if (objCustomerLoan.CustomerLoanId > 0)
            {
              List<CustomerLoanTxn> lstCustomerLoanTxn = new List<CustomerLoanTxn>();
              lstCustomerLoanTxn = _mapper.Map<List<CustomerLoanTxn>>(loan.lsttenure);
              foreach (var tenure in lstCustomerLoanTxn)
              {
                tenure.CreatedBy = userId;
                tenure.CreatedDate = DateTime.Now;
                tenure.CustomerLoanId = objCustomerLoan.CustomerLoanId;
                _context.CustomerLoanTxn.Add(tenure);
                _context.SaveChanges();
              }
            }
          }

          #endregion


          #region AgentBalanceMst

          AgentBalanceMst objAgentBalanceMst = _context.AgentBalanceMst.FirstOrDefault(x => x.AgentId == userId);
          if (objAgentBalanceMst != null)
          {
            objAgentBalanceMst.Lb = objAgentBalanceMst.Lb - loan.LoanAmount;
            objAgentBalanceMst.UpdatedBy = userId;
            objAgentBalanceMst.UpdatedDate = DateTime.Now;
            _context.Entry(objAgentBalanceMst).State = EntityState.Modified;
            _context.SaveChanges();
          }

          #endregion

          #region Upload Customer Loan Doc

          if (loanrequest.uploadDoc != null)
          {
            foreach (var file in loanrequest.uploadDoc)
            {
              CustomerLoanDocumentDtl docDtl = new CustomerLoanDocumentDtl();
              if (file.filedata == null && !string.IsNullOrEmpty(file.Filename))
              {
                //return BadRequest("Empty File");
                docDtl.CustomerLoanId = objCustomerLoan.CustomerLoanId;
                docDtl.DocumentTypeId = file.DocType;
                docDtl.FileName = file.Filename;
                docDtl.UploadedDate = DateTime.Now;
                docDtl.UploadedBy = userId;
                lstLoanDocument.Add(docDtl);
              }
              else if (file.filedata != null)
              {
                //if (file.Length > 2 * 1024 * 1024) return BadRequest("Max file size exceeded.");
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadFilesPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(uploadFilesPath))
                {
                  Directory.CreateDirectory(uploadFilesPath);
                }

                var fileName = Path.GetFileNameWithoutExtension(file.filedata.FileName) + "-" + Guid.NewGuid().ToString() + Path.GetExtension(file.filedata.FileName);
                var filePath = Path.Combine(uploadFilesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await file.filedata.CopyToAsync(stream);
                }

                docDtl.CustomerLoanId = objCustomerLoan.CustomerLoanId;
                docDtl.DocumentTypeId = file.DocType;
                docDtl.FileName = fileName;
                docDtl.UploadedDate = DateTime.Now;
                docDtl.UploadedBy = userId;
                lstLoanDocument.Add(docDtl);
              }
              else
              {
                break;
              }
            }
            _context.CustomerLoanDocumentDtl.AddRange(lstLoanDocument);
            _context.SaveChanges();
          }

          #endregion
        }
      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(new { success = true });
    }

    [HttpGet("download")]
    public HttpResponseMessage Download()
    {
      //if (filename == null)
      //    return Content("filename not present");

      string filename = _context.CustomerLoanDocumentDtl.Where(q => q.CustomerLoanId == 40).FirstOrDefault().FileName;

      string folderName = "CustomerDoc";
      string webRootPath = _hostingEnvironment.WebRootPath;
      string uploadFilesPath = Path.Combine(webRootPath, folderName);
      var filepath = Path.Combine(uploadFilesPath, filename);

      //var memory = new MemoryStream();
      //using (var stream = new FileStream(filepath, FileMode.Open))
      //{
      //    await stream.CopyToAsync(memory);
      //}
      //memory.Position = 0;
      //return File(memory, "application/pdf", Path.GetFileName(filepath));

      //Copy the source file stream to MemoryStream and close the file stream
      MemoryStream responseStream = new MemoryStream();
      Stream fileStream = System.IO.File.Open(filepath, FileMode.Open);

      fileStream.CopyTo(responseStream);
      fileStream.Close();
      responseStream.Position = 0;

      HttpResponseMessage response = new HttpResponseMessage();
      response.StatusCode = HttpStatusCode.OK;

      //Write the memory stream to HttpResponseMessage content
      response.Content = new StreamContent(responseStream);
      string contentDisposition = string.Concat("attachment; filename=", filename);
      response.Content.Headers.ContentDisposition =
      new ContentDispositionHeaderValue("attachment") { FileName = filename };
      //response.Content.Headers.ContentDisposition =
      //              ContentDispositionHeaderValue.Parse(contentDisposition);
      return response;
    }

    [HttpGet("getuploadedloandoc/{userId}")]
    public IActionResult GetUploadedLoanDoc(int userId)
    {
      List<UploadDoc> lstUploadedDocVM = new List<UploadDoc>();

      var t = _context.CustomerLoan.Where(q => q.CustomerId == userId).Select(q => q.CustomerLoanId).ToList();
      List<CustomerLoanDocumentDtl> lstUploadedDoc = _context.CustomerLoanDocumentDtl.Where(q => t.Contains(q.CustomerLoanId)).ToList();

      string folderName = "CustomerDoc";
      string webRootPath = _hostingEnvironment.WebRootPath;
      string uploadFilesPath = Path.Combine(webRootPath, folderName);
      //var filepath = Path.Combine(uploadFilesPath, filename);
      var result = lstUploadedDoc.GroupBy(q => new { q.FileName, q.DocumentTypeId }).Select(a => a.FirstOrDefault());

      foreach (var item in result)
      {
        var doc = new UploadDoc
        {
          filedata = null,
          Filename = item.FileName,
          DocType = item.DocumentTypeId,
          DownloadPath = string.Format("{0}/CustomerDoc/{1}", _configuration["DevImageDomain"], item.FileName),
          isChecked = true
        };

        lstUploadedDocVM.Add(doc);
      }
      return Ok(lstUploadedDocVM);
    }


    [HttpGet("loanlist/{AgentId}")]
    public IActionResult LoanListByAgent(Int32 AgentId)
    {
      List<CustomerLoanVM> lstCustomerLoanVM = new List<CustomerLoanVM>();
      Int32 userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

      try
      {
        if (AgentId != 0)
        {
          List<CustomerLoan> lstCustomerLoan = _context.CustomerLoan.Where(x => x.AgentId == AgentId).Include(m => m.Customer).Include(m => m.CreatedByNavigation).ToList();
          lstCustomerLoanVM = _mapper.Map<List<CustomerLoanVM>>(lstCustomerLoan);
        }
        else
        {
          List<CustomerLoan> lstCustomerLoan = _context.CustomerLoan.Include(m => m.Customer).Include(m => m.CreatedByNavigation).ToList();
          lstCustomerLoanVM = _mapper.Map<List<CustomerLoanVM>>(lstCustomerLoan);
        }
      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(lstCustomerLoanVM);
    }


    [HttpGet("installmentlist/{customerLoanId}")]
    public IActionResult InstallmentList(Int32 customerLoanId)
    {
      List<InstalmentTxnVM> lstinstalmentTxnVM = new List<InstalmentTxnVM>();
      try
      {
        var currentUser = HttpContext.User;
        Int32 loggedinuserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

        if (customerLoanId != 0)
        {
          List<CustomerLoanTxn> lstCustomerLoanTxn = _context.CustomerLoanTxn.Where(x => x.CustomerLoanId == customerLoanId).OrderBy(x => x.InstallmentDate).ToList();
          lstinstalmentTxnVM = _mapper.Map<List<InstalmentTxnVM>>(lstCustomerLoanTxn);
        }
        else
        {
          int todaydateint = Helper.Helper.ConvertDateToInt(DateTime.Now.Date);
          int weekdateint = Helper.Helper.ConvertDateToInt(DateTime.Now.AddDays(7).Date);
          List<CustomerLoanTxn> lstCustomerLoanTxn = _context.CustomerLoanTxn.Include(q => q.CustomerLoan).ThenInclude(q => q.Customer).Where(x => x.InstallmentDateInt >= todaydateint && x.InstallmentDateInt <= weekdateint && x.IsPaid == false && x.CreatedBy == loggedinuserId).OrderBy(x => x.InstallmentDate).ToList();
          lstinstalmentTxnVM = _mapper.Map<List<InstalmentTxnVM>>(lstCustomerLoanTxn);
        }
      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(lstinstalmentTxnVM);
    }

    [HttpGet("getinstallment/{txnId}")]
    public IActionResult GetInstallment(int txnId)
    {
      if (_context.CustomerLoanTxn.Find(txnId).IsPaid)
      {
        return BadRequest();
      }
      var data = _context.CustomerLoanTxn.Include(q => q.CustomerLoan).ThenInclude(q => q.Customer);
      var txnData = data.FirstOrDefault(q => q.TransactionId == txnId);

      var installmentsCount = _context.CustomerLoanTxn.Where(q => q.CustomerLoanId == txnData.CustomerLoanId).ToList().TakeWhile(q => q.TransactionId != txnId).Count();

      InstalmentTxnVM installmentVM = new InstalmentTxnVM
      {
        InstallmentNo = installmentsCount + 1,
        TransactionId = txnId,
        LoanAmount = txnData.CustomerLoan.LoanAmount,
        InstallmentDate = txnData.InstallmentDate,
        InstallmentAmount = txnData.InstallmentAmount,
        CustomerLoanId = txnData.CustomerLoanId,
        CustomerName = txnData.CustomerLoan.Customer.Firstname + ' ' + txnData.CustomerLoan.Customer.Middlename + ' ' + txnData.CustomerLoan.Customer.Lastname
      };

      return Ok(installmentVM);

      //return Ok(new {
      //    transactionid = txnId,
      //    loanamount = txnData.CustomerLoan.LoanAmount,
      //    installmentdate = txnData.InstallmentDate,
      //    installmentamount = txnData.InstallmentAmount,
      //    customerloanid = txnData.CustomerLoanId,
      //    customername = txnData.CustomerLoan.Customer.Firstname + ' ' + txnData.CustomerLoan.Customer.Middlename + ' ' + txnData.CustomerLoan.Customer.Lastname
      //});
    }

    [HttpPost("markpaid")]
    public IActionResult MarkInstallmentPaid([FromBody]InstalmentTxnVM data)
    {
      if (data.PaidAmount <= 0)
      {
        return BadRequest();
      }
      //InstalmentTxnVM data = new InstalmentTxnVM();
      //if (!string.IsNullOrEmpty(req))
      //{
      //   data = JsonConvert.DeserializeObject<InstalmentTxnVM>(req);
      //}

      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);
      bool isPrePay = false;
      bool isLatepay = false;
      decimal newInstallAmount = 0;
      bool IsFullpayment = true;

      CustomerLoanTxn txnData = _context.CustomerLoanTxn.Find(data.TransactionId);

      if (DateTime.Now.Date < txnData.InstallmentDate.Date)
      {
        isPrePay = true;
      }
      else if (DateTime.Now.Date > txnData.InstallmentDate.Date)
      {
        isLatepay = true;
      }

      if (data.PaidAmount < data.InstallmentAmount)
      {
        IsFullpayment = false;
        decimal remainAmnt = data.InstallmentAmount - data.PaidAmount;
        int unpaidInstallments = _context.CustomerLoanTxn.Where(q => q.CustomerLoanId == data.CustomerLoanId && q.IsPaid == false).Count();
        newInstallAmount = data.InstallmentAmount + (remainAmnt / unpaidInstallments);
      }

      txnData.BankName = data.BankName;
      txnData.ChequeNo = data.ChequeNo;
      txnData.CreatedBy = userId;
      txnData.CreatedDate = DateTime.Now;
      txnData.IsPaid = true;
      txnData.IsLatePay = isLatepay;
      txnData.IsPrePay = isPrePay;
      txnData.PaidAmount = data.PaidAmount;
      txnData.PaidDate = DateTime.Now;
      txnData.PaymentMethodId = data.PaymentMethodId;
      txnData.Remarks = data.Remarks;

      _context.Entry(txnData).State = EntityState.Modified;
      _context.SaveChanges();

      int totalUnpaid = _context.CustomerLoanTxn.Where(q => q.CustomerLoanId == data.CustomerLoanId && q.IsPaid == false).Count();
      if (totalUnpaid <= 0)
      {
        var objCustLoan = _context.CustomerLoan.Find(data.CustomerLoanId);
        objCustLoan.IsPaid = true;
        _context.Entry(objCustLoan).State = EntityState.Modified;
        _context.SaveChanges();
      }

      if (!IsFullpayment)
      {
        List<int> ids = _context.CustomerLoanTxn.Where(q => q.CustomerLoanId == data.CustomerLoanId && q.IsPaid == false).Select(q => q.TransactionId).ToList();
        var updateData = _context.CustomerLoanTxn.Where(q => ids.Contains(q.TransactionId)).ToList();
        updateData.ForEach(q => q.InstallmentAmount = newInstallAmount);
        _context.SaveChanges();
      }

      return Ok(new
      {
        success = true
      });
    }

    [HttpGet("getcustomersbyagent/{agentId}")]
    public IActionResult GetCustomersByAgent(int agentId)
    {
      List<LoginCredentials> loginList = new List<LoginCredentials>();
      List<CustomerViewModel> custList = new List<CustomerViewModel>();

      if (agentId > 0)
        loginList = _context.LoginCredentials.Where(q => q.RoleId == (Int32)Helper.EnumList.Roles.Customer && q.IsDelete == false && q.IsActive == true && q.CreatedBy == agentId).ToList();
      else
        loginList = _context.LoginCredentials.Where(q => q.RoleId == (Int32)Helper.EnumList.Roles.Customer && q.IsDelete == false && q.IsActive == true).ToList();

      custList = _mapper.Map<List<CustomerViewModel>>(loginList);

      return Ok(custList);
    }

  }

}
