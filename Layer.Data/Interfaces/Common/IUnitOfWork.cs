using Layer.Data.Implementations.HRMS.Institute;
using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Data.Interfaces.HRMS.Nothi;
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
        IUserRolesRepository UserRolesRepository { get; }
        IModulesRepository ModulesRepository { get; }
        IScreenRepository ScreenRepository { get; }
        IRoleWiseScreenPermissionRepository RoleWiseScreenPermissionRepository { get; }
        ISubModulesRepository SubModulesRepository { get; }
        ISubModuleSectionsRepository SubModuleSectionsRepository { get; }
        IUserWiseOtherScreenRepository UserWiseOtherScreenRepository { get; }
        IUserRepository UserRepository { get; }

        IProfessionalInterestRepository ProfessionalInterestRepository { get; }

        IInstituteRepository InstituteRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeProfessionalInterestRepository EmployeeProfessionalInterestRepository { get; }
        IEmployeeExpertiseAreaRepository EmployeeExpertiseAreaRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ICourseRepository CourseRepository { get; }

        IAreaOfExpertiseRepository AreaOfExpertiseRepository { get; }
        IEmployeeWiseProfileSectionRepository EmployeeWiseProfileSectionRepository { get; }
        IProfileSectionRepository ProfileSectionRepository { get; }

        INothiDetailsRepository NothiDetailsRepository { get; }
        INothiMovementDetailsRepository NothiMovementDetailsRepository { get; }
        INothiMovementRepository NothiMovementRepository { get; }
        INothiTypeRepository NothiTypeRepository { get; }


    }
}
