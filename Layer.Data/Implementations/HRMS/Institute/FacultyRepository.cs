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
    public class FacultyRepository : DataCommon, IFacultyRepository
    {
        public FacultyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Faculty entity)
        {
            var query = CRUD<Faculty>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Faculty>.Delete(o => o.FacultyId == o.FacultyId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { FacultyId = id, InstituteId = instituteId }).Single();
        }

        public Faculty Get(int id, int instituteId)
        {
            var query = CRUD<Faculty>.Select(o => o.FacultyId == o.FacultyId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Faculty>(query, new { FacultyId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<Faculty> GetAll(int instituteId)
        {
            var query = CRUD<Faculty>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Faculty>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<FacultyViewModel> GetAllFaculty(int instituteId)
        {
            int? id = 0;
            if (instituteId == 0)
            {
                id = null;
            }
            else {
                id = instituteId;
            }
            var query = @"SELECT F.FacultyId,F.FirstName,F.MiddleName,F.LastName,F.FacultyFullName,F.DepartmentId,F.InstituteId,F.DesignationId,F.DateOfBirth,F.Gender,F.Education,F.Address,F.Email,F.Mobile,ISNULL(F.Image, (CASE WHEN F.Gender=1 Then 'male.png' When F.Gender=2 then 'female.png' else 'other.png' end) )Image,F.Experience,F.IsActive,F.AddedByUserId,F.AddedDate,F.UpdatedByUserId,F.UpdatedDate, D.DeptName DepartmentName, De.DesignationName

		                FROM Institute.Faculty F
		                INNER JOIN Institute.Department D ON D.DepartmentId=F.DepartmentId AND D.InstituteId=F.InstituteId
		                INNER JOIN Institute.Designation De ON De.DesignationId=F.DesignationId AND De.InstituteId=F.InstituteId
		                WHERE 
		                F.InstituteId=ISNULL(@InstituteId, F.InstituteId)";
            return _dbContext._connection.Query<FacultyViewModel>(query, new
            {
                InstituteId = id
            });
        }

        public int Update(Faculty entity)
        {
            var query = CRUD<Faculty>.Update(o => o.FacultyId == o.FacultyId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
