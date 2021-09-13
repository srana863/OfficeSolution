using Layer.Data.Implementations.HRMS.Emp;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Emp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DashboardController()
        {
            _departmentRepository = new DepartmentRepository(_dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll() 
        {
            _dbContext.Open();
            var data = _departmentRepository.GetAll(2);
            _dbContext.Close();
            return PartialView("_GetAll",data);
        }

    }
}
