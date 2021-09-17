using Layer.Data.Helpers;
using Layer.Data.Implementations.HRMS.Emp;
using Layer.Data.Implementations.HRMS.Security;
using Layer.Data.Interfaces;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IUserRolesRepository UserRolesRepository { get; }
        public IModulesRepository ModulesRepository { get; }
        public IScreenRepository ScreenRepository { get; }
        public IRoleWiseScreenPermissionRepository RoleWiseScreenPermissionRepository { get; }
        public ISubModulesRepository SubModulesRepository { get; }
        public ISubModuleSectionsRepository SubModuleSectionsRepository { get; }
        public IUserWiseOtherScreenRepository UserWiseOtherScreenRepository { get; }

        public UnitOfWork(DbContext _dbContext)
        {
            DepartmentRepository = new DepartmentRepository(_dbContext);
            UserRolesRepository = new UserRolesRepository(_dbContext);
            ModulesRepository = new ModulesRepository(_dbContext);
            ScreenRepository = new ScreenRepository(_dbContext);
            RoleWiseScreenPermissionRepository = new RoleWiseScreenPermissionRepository(_dbContext);
            SubModulesRepository = new SubModulesRepository(_dbContext);
            SubModuleSectionsRepository = new SubModuleSectionsRepository(_dbContext);
            UserWiseOtherScreenRepository = new UserWiseOtherScreenRepository(_dbContext);
        }
    }
}
