using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Models;

namespace LoanMgntAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CommonController : ControllerBase
  {
    private IConfiguration _configuration;
    private LoanManagementContext _dbContext;
    private IMapper _mapper;

    public CommonController(IConfiguration configuration, LoanManagementContext dbContext, IMapper mapper)
    {
      _configuration = configuration;
      _dbContext = dbContext;
      _mapper = mapper;
    }

    [HttpGet]
    [Route("getstate")]
    public IActionResult StateList()
    {
      List<StateMaster> lstStateMaster = new List<StateMaster>();
      try
      {
        lstStateMaster = _dbContext.StateMaster.ToList();
      }
      catch (Exception ex)
      {

      }
      return Ok(lstStateMaster);
    }

    [HttpGet]
    [Route("getcity")]
    public IActionResult CityList(Int32 stateId)
    {
      List<DrpResponce> drpResponseList = new List<DrpResponce>();
      try
      {
        List<CityMaster> lstCityMaster = _dbContext.CityMaster.Where(x => x.StateId == stateId).ToList();
        foreach (var item in lstCityMaster)
        {
          DrpResponce drpResponce = new DrpResponce();
          drpResponce.Id = item.CityId;
          drpResponce.Name = item.CityName;
          drpResponseList.Add(drpResponce);
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(drpResponseList);
    }

    [HttpGet]
    [Route("getarea")]
    public IActionResult AreaList(Int32 cityId)
    {
      List<DrpResponce> drpResponseList = new List<DrpResponce>();
      try
      {
        List<AreaMaster> lstAreaMaster = _dbContext.AreaMaster.Where(x => x.CityId == cityId).ToList();
        foreach (var item in lstAreaMaster)
        {
          DrpResponce drpResponce = new DrpResponce();
          drpResponce.Id = item.AreaId;
          drpResponce.Name = item.ZipCode + " ( " + item.AreaName + " )";
          drpResponseList.Add(drpResponce);
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(drpResponseList);
    }


    [HttpGet]
    [Route("getAgent")]
    public IActionResult getAgent()
    {
      List<RegistrationVM> lstRegistrationVM = new List<RegistrationVM>();
      try
      {
        List<LoginCredentials> lstLoginCredentials = _dbContext.LoginCredentials.Where(x => x.RoleId == (Int32)Helper.EnumList.Roles.Agent && x.IsActive == true && x.IsDelete == false).ToList();
        foreach (var itm in lstLoginCredentials)
        {
          RegistrationVM objRegistrationVM = new RegistrationVM();
          objRegistrationVM = _mapper.Map<RegistrationVM>(itm);
          lstRegistrationVM.Add(objRegistrationVM);
        }
      }
      catch (Exception ex)
      { }
      return Ok(lstRegistrationVM);
    }

    [HttpGet]
    [Route("getloantype")]
    public IActionResult getLoanType()
    {
      List<LoanTypeVM> lstLoanTypeVM = new List<LoanTypeVM>();
      try
      {
        List<LoanTypeMaster> lstLoanTypeMaster = _dbContext.LoanTypeMaster.ToList();
        if (lstLoanTypeMaster.Count > 0)
        {
          lstLoanTypeVM = _mapper.Map<List<LoanTypeVM>>(lstLoanTypeMaster);
        }
      }
      catch (Exception ex)
      {

      }
      return Ok(lstLoanTypeVM);
    }

    [HttpGet("getdocumenttypes")]
    public IActionResult GetAllDocumentTypes()
    {
      List<DocumentTypeMaster> lstDocType = new List<DocumentTypeMaster>();
      try
      {
        lstDocType = _dbContext.DocumentTypeMaster.Where(q => q.IsDeleted == false).ToList();
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status417ExpectationFailed);
      }
      return Ok(lstDocType);
    }


   


  }
}
