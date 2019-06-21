using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using LoanMgntAPI.Helper;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Models;


namespace LoanMgntAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private IConfiguration _configuration;
    private LoanManagementContext _dbContext;


    public AccountController(IConfiguration configuration, LoanManagementContext dbContext)
    {
      _dbContext = dbContext;
      _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel req)
    {
      try
      {
        LoginCredentials user = new LoginCredentials();
        LoginResponseModel response = new LoginResponseModel();
        StringConstants stringConstants = new StringConstants();
        if (ModelState.IsValid)
        {
          //string password = Helper.Helper.Encrypt(req.password);
          user = _dbContext.LoginCredentials.FirstOrDefault(q => q.EmailId == req.email && q.Password == req.password);
          if (user != null)
          {
            if (user.IsActive == true)
            {
              var token = Helper.Helper.Token(_configuration["Issuer"], _configuration["Audience"], _configuration["SigninKey"], req.email, user.UserId.ToString(), user.RoleId);
              response.Token = new JwtSecurityTokenHandler().WriteToken(token);
              response.StatusCode = (int)Helper.EnumList.ResponseType.Success;
            }
            else
            {
              response.StatusCode = (int)Helper.EnumList.ResponseType.Isinactive;
            }
          }
          else
          {
            response.StatusCode = (int)Helper.EnumList.ResponseType.Error;
            response.Message = stringConstants.LoginCredentailWrong;
          }
        }
        return Ok(response);
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.ExpectationFailed);
      }

    }

  }
}
