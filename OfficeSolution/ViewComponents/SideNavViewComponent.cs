using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.ViewComponents
{
    public class SideNavViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public DbContext _dbContext;
        public SideNavViewComponent()
        {
            _dbContext = new DbContext(AppSetting.DefaultConnection, "System.Data.SqlClient");
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        public IViewComponentResult Invoke(int subModuleId)
        {
            _dbContext.Open();
            var res = _unitOfWork.SubModuleSectionsRepository.GetSectionsWithScreen(subModuleId);
            _dbContext.Close();
            return View(res);
        }
    }
}
