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
    }
    public interface IScreenRepository : IGenericRepository<Screen>
    {
    }
    public interface ISubModulesRepository : IGenericRepository<SubModules>
    {
        IEnumerable<SubModulesViewModel> GetAllWithParent(int orgId);
    }
    public interface ISubModuleSectionsRepository : IGenericRepository<SubModuleSections>
    {
        IEnumerable<SubModuleSectionsViewModel> GetAllWithParent(int orgId);
    }
    public interface IUserWiseOtherScreenRepository : IGenericRepository<UserWiseOtherScreen>
    {
    }

}
