using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Interfaces.HRMS.Security
{
    public interface IUserRolesRepository: IGenericRepository<UserRoles>
    {
    }
    public interface IModulesRepository : IGenericRepository<Modules>
    {
    }
    public interface IRoleWiseScreenPermissionRepository : IGenericRepository<RoleWiseScreenPermission>
    {
    }
    public interface IScreenRepository : IGenericRepository<Screen>
    {
    }
    public interface ISubModulesRepository : IGenericRepository<SubModules>
    {
    }
    public interface ISubModuleSectionsRepository : IGenericRepository<SubModuleSections>
    {
    }
    public interface IUserWiseOtherScreenRepository : IGenericRepository<UserWiseOtherScreen>
    {
    }

}
