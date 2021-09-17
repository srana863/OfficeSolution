using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Security
{
    public class ScreenRepository : DataCommon, IScreenRepository
    {
        public ScreenRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Screen entity)
        {
            var query = CRUD<Screen>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<Screen>.Delete(o => o.ScreenCode == o.ScreenCode && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { ScreenCode = id, OrgId = orgId }).Single();
        }

        public Screen Get(int id, int orgId)
        {
            var query = CRUD<Screen>.Select(o => o.ScreenCode == o.ScreenCode && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Screen>(query, new { ScreenCode = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<Screen> GetAll(int orgId)
        {
            var query = CRUD<Screen>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Screen>(query, new { OrgId = orgId });
        }

        public int Update(Screen entity)
        {
            var query = CRUD<Screen>.Update(o => o.ScreenCode == o.ScreenCode && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}


