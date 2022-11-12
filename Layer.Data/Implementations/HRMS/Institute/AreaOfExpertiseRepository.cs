using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
{
    public class AreaOfExpertiseRepository : DataCommon, IAreaOfExpertiseRepository
    {
        public AreaOfExpertiseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(AreaOfExpertise entity)
        {
            var query = CRUD<AreaOfExpertise>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<AreaOfExpertise>.Delete(o => o.ExpertiseId == o.ExpertiseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { ExpertiseId = id, InstituteId = instituteId }).Single();
        }

        public AreaOfExpertise Get(int id, int instituteId)
        {
            var query = CRUD<AreaOfExpertise>.Select(o => o.ExpertiseId == o.ExpertiseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<AreaOfExpertise>(query, new { ExpertiseId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<AreaOfExpertise> GetAll(int instituteId)
        {
            var query = CRUD<AreaOfExpertise>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<AreaOfExpertise>(query, new { InstituteId = instituteId });
        }

        public int Update(AreaOfExpertise entity)
        {
            var query = CRUD<AreaOfExpertise>.Update(o => o.ExpertiseId == o.ExpertiseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

