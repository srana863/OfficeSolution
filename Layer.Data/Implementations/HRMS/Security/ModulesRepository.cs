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
    public class ModulesRepository : DataCommon, IModulesRepository
    {
        public ModulesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Modules entity)
        {
            var query = CRUD<Modules>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<Modules>.Delete(o => o.ModuleId == o.ModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { ModuleId = id, OrgId = orgId }).Single();
        }

        public Modules Get(int id, int orgId)
        {
            var query = CRUD<Modules>.Select(o => o.ModuleId == o.ModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Modules>(query, new { ModuleId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<Modules> GetAll(int orgId)
        {
            var query = CRUD<Modules>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Modules>(query, new { OrgId = orgId });
        }

        public int Update(Modules entity)
        {
            var query = CRUD<Modules>.Update(o => o.ModuleId == o.ModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
