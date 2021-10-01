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
        IEnumerable<RoleWiseScreenPermissionViewModel> GetAllWithParent(int orgId, int roleId, int? moduleId, int? subModuleId);
    }
    public interface IScreenRepository : IGenericRepository<Screen>
    {
        IEnumerable<Model.ViewModel.Security.ScreenViewModel> GetAllWithParent(int orgId);
    }
    public interface ISubModulesRepository : IGenericRepository<SubModules>
    {
        IEnumerable<SubModulesViewModel> GetAllWithParent(int orgId);
    }
    public interface ISubModuleSectionsRepository : IGenericRepository<SubModuleSections>
    {
        IEnumerable<SubModuleSectionsViewModel> GetAllWithParent(int orgId);
        IEnumerable<SectionViewModel> GetSectionsWithScreen(int subModuleId);
    }
    public interface IUserWiseOtherScreenRepository : IGenericRepository<UserWiseOtherScreen>
    {
        IEnumerable<UserWiseOtherScreenViewModel> GetAllWithParent(int orgId, int userId, int? moduleId, int? subModuleId);
    }

}
