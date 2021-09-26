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
    public class OrgTradingHoursRepository : DataCommon, IOrgTradingHoursRepository
    {
        public OrgTradingHoursRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(OrgTradingHours entity)
        {
            var query = CRUD<OrgTradingHours>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<OrgTradingHours>.Delete(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SL = id, OrgId = orgId }).Single();
        }

        public OrgTradingHours Get(int id, int orgId)
        {
            var query = CRUD<OrgTradingHours>.Select(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgTradingHours>(query, new { SL = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<OrgTradingHours> GetAll(int orgId)
        {
            var query = CRUD<OrgTradingHours>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrgTradingHours>(query, new { OrgId = orgId });
        }

        public int Update(OrgTradingHours entity)
        {
            var query = CRUD<OrgTradingHours>.Update(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
