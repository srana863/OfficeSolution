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
    public class FacultyProfessionalInterestRepository : DataCommon, IFacultyProfessionalInterestRepository
    {
        public FacultyProfessionalInterestRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(FacultyProfessionalInterest entity)
        {
            var query = CRUD<FacultyProfessionalInterest>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<FacultyProfessionalInterest>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public FacultyProfessionalInterest Get(int id, int instituteId)
        {
            var query = CRUD<FacultyProfessionalInterest>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyProfessionalInterest>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<FacultyProfessionalInterest> GetAll(int instituteId)
        {
            var query = CRUD<FacultyProfessionalInterest>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyProfessionalInterest>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<FacultyProfessionalInterestViewModel> GetAllFacultyProfessionalInterest(int facultyId, int instituteId)
        {
            var query = @"SELECT FPI.SL,FPI.InstituteId,FPI.FacultyId,FPI.ProfInterestId,FPI.AddedByUserId,FPI.AddedDate,FPI.UpdatedByUserId,FPI.UpdatedDate,PoI.ProfInterestTitle,PoI.ProfInterestDetails
                FROM Institute.FacultyProfessionalInterest FPI
                INNER JOIN Institute.ProfessionalInterest PoI ON PoI.ProfInterestId=FPI.ProfInterestId AND PoI.InstituteId=FPI.InstituteId
                WHERE
                FPI.FacultyId=@FacultyId AND FPI.InstituteId=@InstituteId";
            return _dbContext._connection.Query<FacultyProfessionalInterestViewModel>(query, new { FacultyId=facultyId, InstituteId = instituteId });
        }

        public int Update(FacultyProfessionalInterest entity)
        {
            var query = CRUD<FacultyProfessionalInterest>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
