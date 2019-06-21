using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoanMgntAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility.Models;

namespace LoanMgntAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private LoanManagementContext _dbContext;
        private IMapper _mapper;

        public MenuController(LoanManagementContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet("roles")]
        public IActionResult GetRoleList()
        {
            var roleList = _dbContext.RoleMaster.Where(x => x.IsActive == true).ToList();
            var result = _mapper.Map<List<RoleViewModel>>(roleList);
            return Ok(result);
        }

        [HttpGet("role/{roleId}")]
        public IActionResult GetRole(long roleId)
        {
            RoleMaster role = _dbContext.RoleMaster.FirstOrDefault(x => x.IsActive == true && x.RoleId == roleId);
            var result = _mapper.Map<RoleViewModel>(role);
            return Ok(result);
        }

        #region Role Rights Management

        [HttpGet]
        [Route("rolerights/{roleId}")]
        public IActionResult GetRoleRights(long roleId)
        {
            List<RoleRightsViewModel> roleRightsAC = new List<RoleRightsViewModel>();
            List<Link> mstLinkList = _dbContext.Link.Where(x => x.IsActive == true && x.ModuleId != 1).Include(x => x.Module).OrderBy(x => x.ViewIndex).ToList();
            foreach (var link in mstLinkList)
            {
                RoleRightsViewModel roleRights = new RoleRightsViewModel();
                RoleRight mstRoleRights = _dbContext.RoleRight.FirstOrDefault(x => x.RoleId == roleId && x.LinkId == link.LinkId);
                if (mstRoleRights != null)
                {
                    roleRights = _mapper.Map<RoleRightsViewModel>(mstRoleRights);
                }
                roleRights.LinkId = link.LinkId;
                roleRights.RoleId = roleId;
                roleRights.ModuleName = link.Module.ModuleName;
                roleRights.Title = link.Title;
                roleRightsAC.Add(roleRights);
            }

            return Ok(roleRightsAC);
        }


        [HttpPost("rolerights")]
        public IActionResult GetRoleRights([FromBody] List<RoleRightsViewModel> roleRightsAC)
        {
            var currentUser = HttpContext.User;
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "user_id").Value;
            //return Ok(await _iRoleRepository.UpdateRoleRights(Convert.ToInt64(userId), roleRightsAC));
            return Ok();
        }

        #endregion


        #region Menu Binding

        [Authorize]
        [HttpGet("list")]
        public IActionResult GetMenuList()
        {
            var currentUser = HttpContext.User;
            string roleId = currentUser.Claims.FirstOrDefault(c => c.Type == "role_id").Value;
            int newRoleId = roleId != "" ? Convert.ToInt32(roleId) : 0;

            List<MenuLinkViewModel> menuLinkACList = new List<MenuLinkViewModel>();

            var moduleLink = (from mstModule in _dbContext.ModuleMaster
                              join linkModule in _dbContext.Link on mstModule.ModuleId equals linkModule.ModuleId
                              join roleRights in _dbContext.RoleRight on linkModule.LinkId equals roleRights.LinkId
                              where roleRights.RoleId == newRoleId && linkModule.IsActive == true && roleRights.IsView == true
                              select new NavigationMenuViewModel
                              {
                                  ViewIndex = Convert.ToInt32(mstModule.ViewIndex),
                                  IconName = mstModule.IconName,
                                  IsSinglePage = linkModule.IsSinglePage,
                                  ModuleId = mstModule.ModuleId,
                                  ModuleName = mstModule.ModuleName,
                                  ParentId = linkModule.ParentId,
                                  RouteLink = linkModule.RouteLink,
                                  LinkId = linkModule.LinkId,
                                  Title = linkModule.Title,
                                  IsAdd = roleRights.IsAdd,
                                  IsEdit = roleRights.IsEdit,
                                  IsDelete = roleRights.IsDelete,
                                  IsView = roleRights.IsView,
                                  IsChangeStatus = roleRights.IsChangeStatus,
                                  LinkViewIndex = linkModule.ViewIndex
                              }).OrderBy(x => x.ViewIndex).ThenBy(x => x.LinkViewIndex).GroupBy(x => x.ModuleId).ToList();

            foreach (var item in moduleLink)
            {
                foreach (var subItem in item)
                {
                    if (menuLinkACList.Count(x => x.ModuleId == subItem.ModuleId) == 0)
                    {
                        MenuLinkViewModel menuLinkAC = new MenuLinkViewModel();

                        if (subItem.IsSinglePage && subItem.IsView)
                        {
                            menuLinkAC.IconName = subItem.IconName;
                            menuLinkAC.RouteLink = subItem.RouteLink;
                            menuLinkAC.IsAdd = subItem.IsAdd;
                            menuLinkAC.IsEdit = subItem.IsEdit;
                            menuLinkAC.IsDelete = subItem.IsDelete;
                            menuLinkAC.IsView = subItem.IsView;
                            menuLinkAC.IsChangeStatus = subItem.IsChangeStatus;
                            menuLinkAC.Title = subItem.Title;
                            menuLinkAC.Home = true;
                            menuLinkAC.ModuleId = subItem.ModuleId;
                            menuLinkACList.Add(menuLinkAC);
                        }
                        else if (subItem.IsView)
                        {
                            menuLinkAC.IconName = subItem.IconName;
                            //menuLinkAC.RouteLink = subItem.RouteLink;
                            menuLinkAC.Title = subItem.ModuleName;
                            //menuLinkAC.IsAdd = subItem.IsAdd;
                            //menuLinkAC.IsEdit = subItem.IsEdit;
                            //menuLinkAC.IsDelete = subItem.IsDelete;
                            //menuLinkAC.IsView = subItem.IsView;
                            //menuLinkAC.IsChangeStatus = subItem.IsChangeStatus;
                            menuLinkAC.Home = false;
                            menuLinkAC.ModuleId = subItem.ModuleId;
                            menuLinkAC.Children = new List<MenuLinkViewModel>();
                            List<Link> linkList = _dbContext.Link.Where(x => x.ModuleId == item.Key && x.IsActive == true).ToList();
                            foreach (var subLink in linkList)
                            {
                                var mstRoleRights = _dbContext.RoleRight.FirstOrDefault(x => x.IsView && x.LinkId == subLink.LinkId && x.RoleId == newRoleId);
                                if (mstRoleRights != null)
                                {
                                    MenuLinkViewModel newSubLink = new MenuLinkViewModel();
                                    newSubLink = _mapper.Map<MenuLinkViewModel>(mstRoleRights);
                                    newSubLink.Title = subLink.Title;
                                    newSubLink.RouteLink = subLink.RouteLink;
                                    newSubLink.Home = false;
                                    menuLinkAC.Children.Add(newSubLink);
                                }
                            }
                            menuLinkACList.Add(menuLinkAC);
                        }
                    }
                }
            }
            return Ok(menuLinkACList);
        }

        #endregion

    }
}