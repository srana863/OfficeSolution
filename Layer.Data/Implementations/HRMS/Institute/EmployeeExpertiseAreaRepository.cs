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
    public class EmployeeExpertiseAreaRepository : DataCommon, IEmployeeExpertiseAreaRepository
    {
        public EmployeeExpertiseAreaRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(EmployeeExpertiseArea entity)
        {
            var query = CRUD<EmployeeExpertiseArea>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<EmployeeExpertiseArea>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public EmployeeExpertiseArea Get(int id, int instituteId)
        {
            var query = CRUD<EmployeeExpertiseArea>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeExpertiseArea>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<EmployeeExpertiseArea> GetAll(int instituteId)
        {
            var query = CRUD<EmployeeExpertiseArea>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeExpertiseArea>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<EmployeeExpertiseAreaViewModel> GetAllEmployeeExpertiseArea(int EmployeeId, int instituteId)
        {
            var query = @"SELECT FEA.SL,FEA.InstituteId,FEA.EmployeeId,FEA.ExpertiseId,FEA.AddedByUserId,FEA.AddedDate,FEA.UpdatedByUserId,FEA.UpdatedDate, AE.ExpertiseTitle,AE.ExpertiseDetails 
		                FROM Institute.EmployeeExpertiseArea FEA
		                INNER JOIN Institute.AreaOfExpertise AE ON AE.ExpertiseId=FEA.ExpertiseId AND AE.InstituteId=FEA.InstituteId
		                WHERE FEA.EmployeeId=@EmployeeId AND FEA.InstituteId=@InstituteId";
            return _dbContext._connection.Query<EmployeeExpertiseAreaViewModel>(query, new { EmployeeId = EmployeeId, InstituteId = instituteId });
        }

        public int Update(EmployeeExpertiseArea entity)
        {
            var query = CRUD<EmployeeExpertiseArea>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
