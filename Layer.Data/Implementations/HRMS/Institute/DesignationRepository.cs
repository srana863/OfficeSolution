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
    public class DesignationRepository : DataCommon, IDesignationRepository
    {
        public DesignationRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Designation entity)
        {
            var query = CRUD<Designation>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Designation>.Delete(o => o.DesignationId == o.DesignationId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { DesignationId = id, InstituteId = instituteId }).Single();
        }

        public Designation Get(int id, int instituteId)
        {
            var query = CRUD<Designation>.Select(o => o.DesignationId == o.DesignationId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Designation>(query, new { DesignationId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<Designation> GetAll(int instituteId)
        {
            var query = CRUD<Designation>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Designation>(query, new { InstituteId = instituteId });
        }

        public int Update(Designation entity)
        {
            var query = CRUD<Designation>.Update(o => o.DesignationId == o.DesignationId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
