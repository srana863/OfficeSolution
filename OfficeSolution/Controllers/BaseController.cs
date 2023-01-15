using Layer.Model.Common;
using Layer.Model.Enums;
using Layer.Model.HRMS.Institute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class BaseController : Controller
    {
        public ReturnMessage _vmReturn;
        public int _returnId;
        public DbContext _dbContext;
        public AppSession session;
        public UserInfoSession userinfo;
        public string controllerName;

        public BaseController()
        {
            _vmReturn = new ReturnMessage();
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            controllerName = ControllerContext.ActionDescriptor.ControllerName;

            //ViewBag.MainUrl = HttpContext.Request.Host.Value;

            var isAutheticated = HttpContext.User.Identity.IsAuthenticated;
            //fake session
            session = new AppSession();
           
            if (isAutheticated)
            {
                userinfo = new UserInfoSession
                {
                    UserId = isAutheticated ? Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value) : 0,
                    InstituteId = isAutheticated ? Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "InstituteId")?.Value) : 0,
                    UserName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value) : null,
                    UserFullName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserFullName")?.Value) : null,
                    DesignationName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DesignationName")?.Value) : null,
                    RoleId = isAutheticated ? Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value) : (int)UserRole.User,
                    InstituteName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "InstituteName")?.Value) : null,
                    DepartmentName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DepartmentName")?.Value) : null,
                    Image = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Image")?.Value) : null,
                    RoleName = isAutheticated ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "RoleName")?.Value) : null,
                    PAOfDeptHead = isAutheticated ? Convert.ToBoolean(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "PAOfDeptHead")?.Value) : false,
                    IsOfficeHead = isAutheticated ? Convert.ToBoolean(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IsOfficeHead")?.Value) : false,
                    DepartmentId = isAutheticated ? Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DepartmentId")?.Value) : 0,
                    IsActive = true

                };

            }
            else {
                userinfo = new UserInfoSession
                {
                    InstituteId = 0,
                    RoleId = (int)UserRole.User
                };
            }
            session.UserInfo = userinfo;
            //need to work here
            if (HttpContext.Session.GetString("appSession") != null)
            {
                //session.UserInfo = fakesession;
                HttpContext.Session.SetString("appSession", JsonConvert.SerializeObject(session));
            }
            else
            {
                // session.UserInfo = fakesession;
                //session = JsonConvert.DeserializeObject<AppSession>(HttpContext.Session.GetString("appSession"));
            }
            base.OnActionExecuting(filterContext);
        }
    }

}
