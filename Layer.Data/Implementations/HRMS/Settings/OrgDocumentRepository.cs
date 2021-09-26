using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Settings
{
    public class OrgDocumentRepository : DataCommon, IOrgDocumentRepository
    {
        public OrgDocumentRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(OrgDocument entity)
        {
            var query = CRUD<OrgDocument>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<OrgDocument>.Delete(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SL = id, OrgId = orgId }).Single();
        }

        public OrgDocument Get(int id, int orgId)
        {
            var query = CRUD<OrgDocument>.Select(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgDocument>(query, new { SL = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<OrgDocument> GetAll(int orgId)
        {
            var query = CRUD<OrgDocument>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgDocument>(query, new { OrgId = orgId });
        }

        public int Update(OrgDocument entity)
        {
            var query = CRUD<OrgDocument>.Update(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}