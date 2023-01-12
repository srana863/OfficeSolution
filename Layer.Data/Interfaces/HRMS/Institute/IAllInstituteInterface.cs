using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Institute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Interfaces.HRMS.Institute
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<EmployeeViewModel> GetAllEmployee(int instituteId);
        EmployeeViewModel GetEmployeeProfile(int EmployeeId, int instituteId);
    }
    public interface IAreaOfExpertiseRepository : IGenericRepository<AreaOfExpertise>
    {
    }
    public interface ICourseRepository : IGenericRepository<Course>
    {
    }
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
    public interface IDesignationRepository : IGenericRepository<Designation>
    {
    }
    public interface IEmployeeExpertiseAreaRepository : IGenericRepository<EmployeeExpertiseArea>
    {
        IEnumerable<EmployeeExpertiseAreaViewModel> GetAllEmployeeExpertiseArea(int EmployeeId, int instituteId);
    }
    public interface IEmployeeProfessionalInterestRepository : IGenericRepository<EmployeeProfessionalInterest>
    {
        IEnumerable<EmployeeProfessionalInterestViewModel> GetAllEmployeeProfessionalInterest(int EmployeeId, int instituteId);
        
    }
    public interface IInstituteRepository : IGenericRepository<Institutes>
    {
    }
    public interface IProfessionalInterestRepository : IGenericRepository<ProfessionalInterest>
    {
    }
    public interface IEmployeeWiseProfileSectionRepository : IGenericRepository<EmployeeWiseProfileSection>
    {
        IEnumerable<EmployeeWiseProfileSectionViewModel> GetEmployeeWiseProfileSectionDetails(int EmployeeId, int instituteId);
        EmployeeWiseProfileSection GetProfileSectionDetails(int profileSectionId, int EmployeeId, int instituteId);
    }
    public interface IProfileSectionRepository : IGenericRepository<ProfileSection>
    {
    }        

}
