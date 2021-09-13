using Layer.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class BaseController : Controller
    {
        public ReturnMessage _vmReturn;
        public DbContext _dbContext;
        public BaseController()
        {
            _vmReturn = new ReturnMessage();
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
        }
    }
}
