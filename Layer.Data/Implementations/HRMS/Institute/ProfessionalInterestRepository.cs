using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
{
    public class ProfessionalInterestRepository : DataCommon, IProfessionalInterestRepository
    {
        public ProfessionalInterestRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(ProfessionalInterest entity)
        {
            var query = CRUD<ProfessionalInterest>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<ProfessionalInterest>.Delete(o => o.ProfInterestId == o.ProfInterestId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { ProfInterestId = id, InstituteId = instituteId }).Single();
        }

        public ProfessionalInterest Get(int id, int instituteId)
        {
            var query = CRUD<ProfessionalInterest>.Select(o => o.ProfInterestId == o.ProfInterestId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<ProfessionalInterest>(query, new { ProfInterestId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<ProfessionalInterest> GetAll(int instituteId)
        {
            var query = CRUD<ProfessionalInterest>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<ProfessionalInterest>(query, new { InstituteId = instituteId });
        }

        public int Update(ProfessionalInterest entity)
        {
            var query = CRUD<ProfessionalInterest>.Update(o => o.ProfInterestId == o.ProfInterestId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
