using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.ViewModel.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
{
    public class EmployeeRepository : DataCommon, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Employee entity)
        {
            var query = CRUD<Employee>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Employee>.Delete(o => o.EmployeeId == o.EmployeeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { EmployeeId = id, InstituteId = instituteId }).Single();
        }

        public Employee Get(int id, int instituteId)
        {
            var query = CRUD<Employee>.Select(o => o.EmployeeId == o.EmployeeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Employee>(query, new { EmployeeId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<Employee> GetAll(int instituteId)
        {
            var query = CRUD<Employee>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Employee>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee(int instituteId)
        {
            int? id = 0;
            if (instituteId == 0)
            {
                id = null;
            }
            else
            {
                id = instituteId;
            }
            var query = @"SELECT F.EmployeeId,F.FirstName,F.MiddleName,F.PAOfDeptHead,F.IsOfficeHead,F.LastName,F.EmployeeFullName,F.DepartmentId,F.InstituteId,F.DesignationId,F.DateOfBirth,F.Gender,F.Education,F.Address,F.Email,F.Mobile,ISNULL(F.Image, (CASE WHEN F.Gender=1 Then 'male.png' When F.Gender=2 then 'female.png' else 'other.png' end) )Image,F.Experience, F.About,F.IsActive,F.AddedByUserId,F.AddedDate,F.UpdatedByUserId,F.UpdatedDate, D.DeptName DepartmentName, De.DesignationName

		                FROM Institute.Employee F
		                INNER JOIN Institute.Department D ON D.DepartmentId=F.DepartmentId AND D.InstituteId=F.InstituteId
		                INNER JOIN Institute.Designation De ON De.DesignationId=F.DesignationId AND De.InstituteId=F.InstituteId
		                WHERE 
		                F.InstituteId=ISNULL(@InstituteId, F.InstituteId)";
            return _dbContext._connection.Query<EmployeeViewModel>(query, new
            {
                InstituteId = id
            });
        }

        public EmployeeViewModel GetEmployeeProfile(int EmployeeId, int instituteId)
        {
            int? id = 0;
            if (instituteId == 0)
            {
                id = null;
            }
            else
            {
                id = instituteId;
            }
            var query = @"SELECT F.EmployeeId,F.FirstName,F.MiddleName,F.PAOfDeptHead,F.IsOfficeHead,F.LastName,F.EmployeeFullName,F.DepartmentId,F.InstituteId,F.DesignationId,F.DateOfBirth,F.Gender,F.Education,F.Address,F.Email,F.Mobile,ISNULL(F.Image, (CASE WHEN F.Gender=1 Then 'male.png' When F.Gender=2 then 'female.png' else 'other.png' end) )Image,F.Experience, F.About,F.IsActive,F.AddedByUserId,F.AddedDate,F.UpdatedByUserId,F.UpdatedDate, D.DeptName DepartmentName, De.DesignationName

		                FROM Institute.Employee F
		                INNER JOIN Institute.Department D ON D.DepartmentId=F.DepartmentId AND D.InstituteId=F.InstituteId
		                INNER JOIN Institute.Designation De ON De.DesignationId=F.DesignationId AND De.InstituteId=F.InstituteId
		                WHERE F.EmployeeId=@EmployeeId AND F.InstituteId=ISNULL(@InstituteId, F.InstituteId)";
            return _dbContext._connection.Query<EmployeeViewModel>(query, new { EmployeeId = EmployeeId, InstituteId = id }).FirstOrDefault();
        }

        public int Update(Employee entity)
        {
            var query = CRUD<Employee>.Update(o => o.EmployeeId == o.EmployeeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
