using Layer.Data.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class DashboardController : BaseController
    {
       // private readonly IDepartmentRepository _departmentRepository;
        public DashboardController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll() 
        {
            _dbContext.Open();
            //var data = _departmentRepository.GetAll(2);
            var data = string.Empty;
            _dbContext.Close();
            return PartialView("_GetAll",data);
        }

    }
}
