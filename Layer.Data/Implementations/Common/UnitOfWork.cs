using Layer.Data.Helpers;
using Layer.Data.Implementations.HRMS.Institute;
using Layer.Data.Implementations.HRMS.Security;
using Layer.Data.Implementations.HRMS.Settings;
using Layer.Data.Interfaces;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Data.Interfaces.HRMS.Institute;
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
        public IUserRolesRepository UserRolesRepository { get; }
        public IModulesRepository ModulesRepository { get; }
        public IScreenRepository ScreenRepository { get; }
        public IRoleWiseScreenPermissionRepository RoleWiseScreenPermissionRepository { get; }
        public ISubModulesRepository SubModulesRepository { get; }
        public ISubModuleSectionsRepository SubModuleSectionsRepository { get; }
        public IUserWiseOtherScreenRepository UserWiseOtherScreenRepository { get; }
        public IUserRepository UserRepository { get; }
        public IProfessionalInterestRepository ProfessionalInterestRepository { get; }
        public IInstituteRepository InstituteRepository { get; }
        public IFacultyRepository FacultyRepository { get; }
        public IFacultyProfessionalInterestRepository FacultyProfessionalInterestRepository { get; }
        public IFacultyExpertiseAreaRepository FacultyExpertiseAreaRepository { get; }
        public IDesignationRepository DesignationRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public IAreaOfExpertiseRepository AreaOfExpertiseRepository { get; }

        public UnitOfWork(DbContext _dbContext)
        {
            UserRolesRepository = new UserRolesRepository(_dbContext);
            ModulesRepository = new ModulesRepository(_dbContext);
            ScreenRepository = new ScreenRepository(_dbContext);
            RoleWiseScreenPermissionRepository = new RoleWiseScreenPermissionRepository(_dbContext);
            SubModulesRepository = new SubModulesRepository(_dbContext);
            SubModuleSectionsRepository = new SubModuleSectionsRepository(_dbContext);
            UserWiseOtherScreenRepository = new UserWiseOtherScreenRepository(_dbContext);
            UserRepository = new UserRepository(_dbContext);
            ProfessionalInterestRepository = new ProfessionalInterestRepository(_dbContext);
            InstituteRepository = new InstituteRepository(_dbContext);
            FacultyRepository = new FacultyRepository(_dbContext);
            FacultyProfessionalInterestRepository = new FacultyProfessionalInterestRepository(_dbContext);
            FacultyExpertiseAreaRepository = new FacultyExpertiseAreaRepository(_dbContext);
            DesignationRepository = new DesignationRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
            CourseRepository = new CourseRepository(_dbContext);
            AreaOfExpertiseRepository = new AreaOfExpertiseRepository(_dbContext);


        }
    }
}
