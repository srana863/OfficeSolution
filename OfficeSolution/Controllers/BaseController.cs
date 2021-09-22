using Layer.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public BaseController()
        {
            _vmReturn = new ReturnMessage();
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //fake session
            session = new AppSession();
            //userinfo = new UserInfoSession { 
            //    UserId= Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value),
            //    OrgId= Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrgId")?.Value),
            //    Username= "srana863",
            //    UserFullName= "Md. Sohel Rana",
            //    Designation= "Maintenance Engineer",
            //    RoleId =1,
            //    IsActive=true
                
            //};
         
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
