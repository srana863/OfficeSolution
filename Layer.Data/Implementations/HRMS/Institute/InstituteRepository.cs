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
    public class InstituteRepository : DataCommon, IInstituteRepository
    {
        public InstituteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Institutes entity)
        {
            var query = CRUD<Institutes>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Institutes>.Delete(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { InstituteId = id}).Single();
        }

        public Institutes Get(int id, int instituteId)
        {
            var query = CRUD<Institutes>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Institutes>(query, new { InstituteId = id}).FirstOrDefault();
        }

        public IEnumerable<Institutes> GetAll(int instituteId)
        {
            var query = CRUD<Institutes>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Institutes>(query, new { InstituteId = instituteId });
        }

        public int Update(Institutes entity)
        {
            var query = CRUD<Institutes>.Update(o => o.InstituteId == o.InstituteId );
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

