using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Institute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Interfaces.HRMS.Institute
{
    public interface IFacultyRepository : IGenericRepository<Faculty>
    {
        IEnumerable<FacultyViewModel> GetAllFaculty(int instituteId);
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
    public interface IFacultyExpertiseAreaRepository : IGenericRepository<FacultyExpertiseArea>
    {
        IEnumerable<FacultyExpertiseAreaViewModel> GetAllFacultyExpertiseArea(int facultyId, int instituteId);
    }
    public interface IFacultyProfessionalInterestRepository : IGenericRepository<FacultyProfessionalInterest>
    {
        IEnumerable<FacultyProfessionalInterestViewModel> GetAllFacultyProfessionalInterest(int facultyId, int instituteId);
        
    }
    public interface IInstituteRepository : IGenericRepository<Institutes>
    {
    }
    public interface IProfessionalInterestRepository : IGenericRepository<ProfessionalInterest>
    {
    }

}
