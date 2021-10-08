using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.ViewModel.Settings;
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
        public IViewComponentResult Invoke(string actionName,string controllerName,string homeActionName,string homeControllerName,bool home=false)
        {
            _dbContext.Open();
            IEnumerable<SectionViewModel> res;
            if (home) {
                res = _unitOfWork.SubModuleSectionsRepository.GetSectionsWithScreen(actionName, controllerName,home);
            }
            else { 
                res = _unitOfWork.SubModuleSectionsRepository.GetSectionsWithScreen(actionName,controllerName);
            }
            _dbContext.Close();
            var sub = new SubModuleViewModel();

            sub.ActionName = homeActionName;
            sub.ControllerName = homeControllerName;

            return View(new SideNavViewModel {SubModuleViewModel= sub , SectionViewModels=res});
        }
    }
}
