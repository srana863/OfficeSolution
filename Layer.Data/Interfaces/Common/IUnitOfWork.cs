using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Data.Interfaces.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.Common
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IUserRolesRepository UserRolesRepository { get; }
        IModulesRepository ModulesRepository { get; }
        IScreenRepository ScreenRepository { get; }
        IRoleWiseScreenPermissionRepository RoleWiseScreenPermissionRepository { get; }
        ISubModulesRepository SubModulesRepository { get; }
        ISubModuleSectionsRepository SubModuleSectionsRepository { get; }
        IUserWiseOtherScreenRepository UserWiseOtherScreenRepository { get; }
        IUsersRepository UsersRepository { get; }
    }
}
