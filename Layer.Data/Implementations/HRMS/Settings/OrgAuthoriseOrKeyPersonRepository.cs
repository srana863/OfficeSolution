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
    public class OrgAuthoriseOrKeyPersonRepository : DataCommon, IOrgAuthoriseOrKeyPersonRepository
    {
        public OrgAuthoriseOrKeyPersonRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(OrgAuthoriseOrKeyPerson entity)
        {
            var query = CRUD<OrgAuthoriseOrKeyPerson>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<OrgAuthoriseOrKeyPerson>.Delete(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SL = id, OrgId = orgId }).Single();
        }

        public OrgAuthoriseOrKeyPerson Get(int id, int orgId)
        {
            var query = CRUD<OrgAuthoriseOrKeyPerson>.Select(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgAuthoriseOrKeyPerson>(query, new { SL = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<OrgAuthoriseOrKeyPerson> GetAll(int orgId)
        {
            var query = CRUD<OrgAuthoriseOrKeyPerson>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgAuthoriseOrKeyPerson>(query, new { OrgId = orgId });
        }

        public int Update(OrgAuthoriseOrKeyPerson entity)
        {
            var query = CRUD<OrgAuthoriseOrKeyPerson>.Update(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}