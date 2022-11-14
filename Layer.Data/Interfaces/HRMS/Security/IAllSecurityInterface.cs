using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Security;
using Layer.Model.ViewModel.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.HRMS.Security
{
    public interface IUserRolesRepository: IGenericRepository<UserRoles>
    {
    }
    public interface IModulesRepository : IGenericRepository<Modules>
    {
        IEnumerable<ModuleViewModel> GetModulesWithSub();
    }
    public interface IRoleWiseScreenPermissionRepository : IGenericRepository<RoleWiseScreenPermission>
    {
        IEnumerable<RoleWiseScreenPermissionViewModel> GetAllWithParent(int InstituteId, int roleId, int? moduleId, int? subModuleId);
        IEnumerable<RoleWiseScreenPermissionViewModel> GetRoleWiseScreen(int roleId, int moduleId);
    }
    public interface IScreenRepository : IGenericRepository<Screen>
    {
        IEnumerable<Model.ViewModel.Security.ScreenViewModel> GetAllWithParent(int InstituteId);
        Screen GetModuleDetailsByControllerName(string controllerName);
    }
    public interface ISubModulesRepository : IGenericRepository<SubModules>
    {
        IEnumerable<SubModulesViewModel> GetAllWithParent(int InstituteId);
    }
    public interface ISubModuleSectionsRepository : IGenericRepository<SubModuleSections>
    {
        IEnumerable<SubModuleSectionsViewModel> GetAllWithParent(int InstituteId);
        IEnumerable<SectionViewModel> GetSectionsWithScreen(string actionName, string controllerName,bool home);
        IEnumerable<SectionViewModel> GetSectionsWithScreen(string actionName, string controllerName);
    }
    public interface IUserWiseOtherScreenRepository : IGenericRepository<UserWiseOtherScreen>
    {
        IEnumerable<UserWiseOtherScreenViewModel> GetAllWithParent(int InstituteId, int userId, int? moduleId, int? subModuleId);
    }

}
