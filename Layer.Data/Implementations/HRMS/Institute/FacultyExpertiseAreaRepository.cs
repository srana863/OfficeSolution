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
    public class FacultyExpertiseAreaRepository : DataCommon, IFacultyExpertiseAreaRepository
    {
        public FacultyExpertiseAreaRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(FacultyExpertiseArea entity)
        {
            var query = CRUD<FacultyExpertiseArea>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<FacultyExpertiseArea>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public FacultyExpertiseArea Get(int id, int instituteId)
        {
            var query = CRUD<FacultyExpertiseArea>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyExpertiseArea>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<FacultyExpertiseArea> GetAll(int instituteId)
        {
            var query = CRUD<FacultyExpertiseArea>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyExpertiseArea>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<FacultyExpertiseAreaViewModel> GetAllFacultyExpertiseArea(int facultyId, int instituteId)
        {
            var query = @"SELECT FEA.SL,FEA.InstituteId,FEA.FacultyId,FEA.ExpertiseId,FEA.AddedByUserId,FEA.AddedDate,FEA.UpdatedByUserId,FEA.UpdatedDate, AE.ExpertiseTitle,AE.ExpertiseDetails 
		                FROM Institute.FacultyExpertiseArea FEA
		                INNER JOIN Institute.AreaOfExpertise AE ON AE.ExpertiseId=FEA.ExpertiseId AND AE.InstituteId=FEA.InstituteId
		                WHERE FEA.FacultyId=@FacultyId AND FEA.InstituteId=@InstituteId";
            return _dbContext._connection.Query<FacultyExpertiseAreaViewModel>(query, new { FacultyId= facultyId,InstituteId = instituteId });
        }

        public int Update(FacultyExpertiseArea entity)
        {
            var query = CRUD<FacultyExpertiseArea>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
