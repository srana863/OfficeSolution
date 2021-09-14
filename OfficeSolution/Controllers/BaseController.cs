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
        public DbContext _dbContext;
        public AppSession session;

        public BaseController()
        {
            _vmReturn = new ReturnMessage();
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            session = JsonConvert.DeserializeObject<AppSession>(HttpContext.Session.GetString("appSession"));

            if (session != null)
            {
                HttpContext.Session.SetString("appSession", JsonConvert.SerializeObject(session));
            }
            else {

                //HttpContext.Session.SetString("appSession", JsonConvert.SerializeObject(session));
            }
            base.OnActionExecuting(filterContext);
        }
    }
    
}
