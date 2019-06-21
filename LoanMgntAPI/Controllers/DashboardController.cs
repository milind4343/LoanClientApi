using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Models;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoanMgntAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashboardController : Controller
  {
    private IConfiguration _configuration;
    private LoanManagementContext _dbContext;
    private IMapper _mapper;

    public DashboardController(IConfiguration configuration, LoanManagementContext dbContext, IMapper mapper)
    {
      _configuration = configuration;
      _dbContext = dbContext;
      _mapper = mapper;
    }

    [HttpGet("dueinstallmentlist")]
    public IActionResult DueInstalmentList()
    {
      List<InstalmentTxnVM> lstinstalmentTxnVM = new List<InstalmentTxnVM>();
      try
      {
        int todaydateint = Helper.Helper.ConvertDateToInt(DateTime.Now.Date);
        int weekdateint = Helper.Helper.ConvertDateToInt(DateTime.Now.AddDays(7).Date);
        List<CustomerLoanTxn> lstCustomerLoanTxn = _dbContext.CustomerLoanTxn.Include(q => q.CustomerLoan).ThenInclude(q => q.Customer).Where(x => x.InstallmentDateInt >= todaydateint && x.InstallmentDateInt <= weekdateint && x.IsPaid == false).OrderBy(x => x.InstallmentDate).ToList();
        lstinstalmentTxnVM = _mapper.Map<List<InstalmentTxnVM>>(lstCustomerLoanTxn);
      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }
      return Ok(lstinstalmentTxnVM);
    }
  }
}
