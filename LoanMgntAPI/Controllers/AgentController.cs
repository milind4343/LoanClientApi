using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoanMgntAPI.Controllers
{
  [Authorize]
  [EnableCors("CORS")]
  [Route("api/[controller]")]
  public class AgentController : Controller
  {
    private IConfiguration _configuration;
    private LoanManagementContext _dbContext;
    private IMapper _mapper;
    private readonly IHostingEnvironment _hostingEnvironment;

    public AgentController(IConfiguration configuration, LoanManagementContext dbContext, IMapper mapper, IHostingEnvironment hostingEnvironment)
    {
      _configuration = configuration;
      _dbContext = dbContext;
      _mapper = mapper;
      _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost]
    [Route("registration")]
    public IActionResult Registration([FromBody] RegistrationVM req)
    {
      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

      LoginCredentials objLoginCredentials = new LoginCredentials();
      try
      {
        if (req != null && req.UserId > 0)
        {
          LoginCredentials getloginCredentials = _dbContext.LoginCredentials.FirstOrDefault(x => x.UserId == req.UserId);
          if (getloginCredentials != null)
          {
            getloginCredentials = _mapper.Map(req, getloginCredentials);
            _dbContext.Entry(getloginCredentials).State = EntityState.Modified;
            _dbContext.SaveChanges();
          }
        }
        else
        {
          objLoginCredentials = _mapper.Map<LoginCredentials>(req);
          objLoginCredentials.Dob = req.Dob.Value.AddDays(1).Date;
          objLoginCredentials.Username = req.EmailId;
          objLoginCredentials.IsActive = true;
          objLoginCredentials.IsDelete = false;
          objLoginCredentials.Profession = "Agent";
          objLoginCredentials.RoleId = (Int32)Helper.EnumList.Roles.Agent;
          objLoginCredentials.CreatedBy = userId;
          objLoginCredentials.CreatedDate = DateTime.Now;
          objLoginCredentials.Password = Helper.Helper.CreateRandomPassword();


          _dbContext.Add(objLoginCredentials);
          _dbContext.SaveChanges();

          if (objLoginCredentials.UserId > 0)
          {
            #region Send Email for Reset password Link
            Dictionary<string, string> replacement = new Dictionary<string, string>();
            replacement.Add("#url", "http://localhost:4200/#/auth/login");
            replacement.Add("#user", objLoginCredentials.Firstname + " " + objLoginCredentials.Lastname);
            replacement.Add("#username", objLoginCredentials.EmailId);
            replacement.Add("#password", Helper.Helper.Decrypt(objLoginCredentials.Password));
            #endregion

            bool IsSendMail = Helper.EmailSender.SendEmail(Convert.ToInt64(Helper.EnumList.EmailTemplateType.AgentRegistration), replacement, objLoginCredentials.EmailId);
            if (IsSendMail)
              objLoginCredentials = _dbContext.LoginCredentials.FirstOrDefault(x => x.UserId == objLoginCredentials.UserId);
            else
              objLoginCredentials = new LoginCredentials();
          }
        }


      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(objLoginCredentials);
    }

    [HttpGet]
    [Route("editAgent")]
    public IActionResult editAgent(long userId)
    {
      RegistrationVM registrationVM = new RegistrationVM();
      try
      {
        LoginCredentials loginCredentials = _dbContext.LoginCredentials.FirstOrDefault(x => x.UserId == userId);
        if (loginCredentials != null)
        {
          RegistrationVM objRegistrationVM = new RegistrationVM();
          registrationVM = _mapper.Map<RegistrationVM>(loginCredentials);
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(registrationVM);
    }

    [HttpGet]
    [Route("changeStatus/{userId}/{isActive}")]
    public IActionResult changeStatus(long userId, bool isActive)
    {
      LoginCredentials loginCredentials = new LoginCredentials();
      try
      {
        loginCredentials = _dbContext.LoginCredentials.FirstOrDefault(x => x.UserId == userId);
        if (loginCredentials != null)
        {
          loginCredentials.IsActive = isActive;
          _dbContext.Entry(loginCredentials).State = EntityState.Modified;
          _dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(loginCredentials);
    }

    [HttpPost]
    [Route("addAgentfund")]
    public IActionResult addAgentfund([FromBody] FundVM fund)
    {
      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);
      AgentFund agentFund = new AgentFund();
      try
      {
        if (fund != null)
        {
          //agentFund = _mapper.Map(fund, agentFund);
          agentFund.AgentId = fund.AgentId;
          agentFund.FundAmount = fund.FundAmount;
          agentFund.IsReceive = fund.IsReceive;
          agentFund.CreatedBy = userId;
          agentFund.Remark = "any";
          agentFund.CreatedDate = DateTime.Now;
          _dbContext.AgentFund.Add(agentFund);
          _dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {

      }
      return Ok();
    }

    //[HttpGet("getAgentfund/{userId}{IsAgent}")]
    [HttpGet("getAgentfund/{IsAgent}/{AgentId}")]
    //public IActionResult getAgentfund(Int32 userId, bool IsAgent = false)
    public IActionResult getAgentfund(bool IsAgent = false, Int32 AgentId = 0)
    {
      FundVMList objFundVMList = new FundVMList();
      try
      {
        //if(IsAgent)
        //{
        Int32 userId = Convert.ToInt32(User.Claims.FirstOrDefault(q => q.Type == "user_id").Value);
        //}

        List<AgentFund> lstagentFundDtl = new List<AgentFund>();
        if (IsAgent)
        {
          lstagentFundDtl = _dbContext.AgentFund.Where(x => x.AgentId == userId).Include(x => x.Agent).Include(x => x.CreatedByNavigation).ToList();
        }
        else
        {
          if (AgentId == 0)
          {
            lstagentFundDtl = _dbContext.AgentFund.Where(x => x.CreatedBy == userId).Include(x => x.Agent).Include(x => x.CreatedByNavigation).ToList();
          }
          else
          {
            lstagentFundDtl = _dbContext.AgentFund.Where(x => x.CreatedBy == userId && x.AgentId == AgentId).Include(x => x.Agent).Include(x => x.CreatedByNavigation).ToList();
          }
        }

        if (lstagentFundDtl.Count > 0)
        {
          List<FundVM> lstFundVM = new List<FundVM>();
          lstFundVM = _mapper.Map<List<FundVM>>(lstagentFundDtl);
          objFundVMList.lstFundVM = lstFundVM;
          objFundVMList.AgentName = lstFundVM.First().AgnetName;
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(objFundVMList);
    }

    [HttpGet]
    [Route("isreceivefund/{agentfundId}/{isreceive}")]
    public IActionResult IsReceivefund(long agentfundId, bool isreceive)
    {
      AgentFund objAgentFund = new AgentFund();
      FundVMList objFundVMList = new FundVMList();
      try
      {
        var currentUser = HttpContext.User;
        Int32 loggedinuserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);

        objAgentFund = _dbContext.AgentFund.FirstOrDefault(x => x.AgentFundId == agentfundId);
        if (objAgentFund != null)
        {
          objAgentFund.IsReceive = true;
          _dbContext.Entry(objAgentFund).State = EntityState.Modified;
          _dbContext.SaveChanges();
        }

        if (objAgentFund.AgentFundId > 0)
        {

          #region Top up walletamount in Logincredential
          LoginCredentials objLoginCredentials = _dbContext.LoginCredentials.FirstOrDefault(x => x.UserId == objAgentFund.AgentId);
          objLoginCredentials.WalletAmount = Convert.ToDecimal(objLoginCredentials.WalletAmount) + objAgentFund.FundAmount;
          _dbContext.Add(objLoginCredentials).State = EntityState.Modified;
          _dbContext.SaveChanges();
          #endregion


          #region Add/Update balance bifurcation in AgentBalanceMst
          AgentBalanceMst objAgentBalanceMst = _dbContext.AgentBalanceMst.FirstOrDefault(x => x.AgentId == loggedinuserId);
          if (objAgentBalanceMst != null)
          {
            objAgentBalanceMst.Lb = objAgentBalanceMst.Lb + objAgentFund.FundAmount;
            _dbContext.Add(objAgentBalanceMst).State = EntityState.Modified;
            _dbContext.SaveChanges();
          }
          else
          {
            AgentBalanceMst addAgentBalanceMst = new AgentBalanceMst();
            addAgentBalanceMst.Lb = objAgentFund.FundAmount;
            addAgentBalanceMst.AgentId = loggedinuserId;
            addAgentBalanceMst.CreatedDate = DateTime.Now;
            addAgentBalanceMst.CreatedBy = loggedinuserId;
            addAgentBalanceMst.Lb = objAgentFund.FundAmount;
            _dbContext.AgentBalanceMst.Add(addAgentBalanceMst);
            _dbContext.SaveChanges();
          }
          #endregion

          List<AgentFund> lstagentFundDtl = _dbContext.AgentFund.Where(x => x.AgentId == agentfundId).Include(x => x.Agent).Include(x => x.CreatedByNavigation).ToList();
          if (lstagentFundDtl.Count > 0)
          {
            List<FundVM> lstFundVM = new List<FundVM>();
            lstFundVM = _mapper.Map<List<FundVM>>(lstagentFundDtl);
            objFundVMList.lstFundVM = lstFundVM;
            objFundVMList.AgentName = lstFundVM.First().AgnetName;
          }
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(objFundVMList);
    }

    [HttpGet]
    [Route("loggedinuser")]
    public IActionResult loggedinuser()
    {
      var currentUser = HttpContext.User;
      Int32 userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value);
      LoggedInUser objLoggedInUser = new LoggedInUser();
      LoggedInUserVM custobj = new LoggedInUserVM();
      string loggedInUser = string.Empty;
      try
      {
        var data = _dbContext.LoginCredentials.FirstOrDefault(q => q.UserId == userId);
        custobj = new LoggedInUserVM();
        custobj = _mapper.Map<LoggedInUserVM>(data);
        if (custobj != null)
        {
          string folderName = "Images";
          string webRootPath = _hostingEnvironment.WebRootPath;
          string imagename = data.ProfileImage;
          var domain = _configuration["DevImageDomain"];

          if (imagename != null)
          {
            string uploadFilesPath = Path.Combine(webRootPath, folderName, imagename);
            if (System.IO.File.Exists(uploadFilesPath))
            {
              custobj.ProfileImageURL = string.Format("{0}/Images/{1}", domain, imagename);
            }
          }
        }
      }
      catch (Exception)
      {

      }
      return Ok(custobj);
    }



  }
}
