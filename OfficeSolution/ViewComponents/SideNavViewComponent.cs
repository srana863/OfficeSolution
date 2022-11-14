using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.ViewModel.Institute;
using Layer.Model.ViewModel.Security;
using Layer.Model.ViewModel.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public IViewComponentResult Invoke(int roleId, string controllerName)
        {
            var data = Enumerable.Empty<RoleWiseScreenPermissionViewModel>();
            try
            {
                _dbContext.Open();
                var moduleDetails = _unitOfWork.ScreenRepository.GetModuleDetailsByControllerName(controllerName);
                if (moduleDetails != null)
                {
                    data = _unitOfWork.RoleWiseScreenPermissionRepository.GetRoleWiseScreen(roleId, moduleDetails.ModuleId);
                }
                
                return View(data);
            }
            catch (Exception)
            {
                return View(data);
            }
            finally
            {
                _dbContext.Close();
            }
        }
    }
}
