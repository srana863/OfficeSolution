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
    public class EmployeeProfessionalInterestRepository : DataCommon, IEmployeeProfessionalInterestRepository
    {
        public EmployeeProfessionalInterestRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(EmployeeProfessionalInterest entity)
        {
            var query = CRUD<EmployeeProfessionalInterest>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<EmployeeProfessionalInterest>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public EmployeeProfessionalInterest Get(int id, int instituteId)
        {
            var query = CRUD<EmployeeProfessionalInterest>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeProfessionalInterest>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<EmployeeProfessionalInterest> GetAll(int instituteId)
        {
            var query = CRUD<EmployeeProfessionalInterest>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeProfessionalInterest>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<EmployeeProfessionalInterestViewModel> GetAllEmployeeProfessionalInterest(int EmployeeId, int instituteId)
        {
            var query = @"SELECT FPI.SL,FPI.InstituteId,FPI.EmployeeId,FPI.ProfInterestId,FPI.AddedByUserId,FPI.AddedDate,FPI.UpdatedByUserId,FPI.UpdatedDate,PoI.ProfInterestTitle,PoI.ProfInterestDetails
                FROM Institute.EmployeeProfessionalInterest FPI
                INNER JOIN Institute.ProfessionalInterest PoI ON PoI.ProfInterestId=FPI.ProfInterestId AND PoI.InstituteId=FPI.InstituteId
                WHERE
                FPI.EmployeeId=@EmployeeId AND FPI.InstituteId=@InstituteId";
            return _dbContext._connection.Query<EmployeeProfessionalInterestViewModel>(query, new { EmployeeId = EmployeeId, InstituteId = instituteId });
        }

        public int Update(EmployeeProfessionalInterest entity)
        {
            var query = CRUD<EmployeeProfessionalInterest>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
