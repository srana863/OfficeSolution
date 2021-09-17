using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Security;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Security
{
    public class SubModulesRepository : DataCommon, ISubModulesRepository
    {
        public SubModulesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(SubModules entity)
        {
            var query = CRUD<SubModules>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<SubModules>.Delete(o => o.SubModuleId == o.SubModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SubModuleId = id, OrgId = orgId }).Single();
        }

        public SubModules Get(int id, int orgId)
        {
            var query = CRUD<SubModules>.Select(o => o.SubModuleId == o.SubModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<SubModules>(query, new { SubModuleId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<SubModules> GetAll(int orgId)
        {
            var query = CRUD<SubModules>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<SubModules>(query, new { OrgId = orgId });
        }

        public IEnumerable<SubModulesViewModel> GetAllWithParent(int orgId)
        {
            var query = @"SELECT SM.SubModuleId,SM.OrgId,SM.ModuleId,SM.SubModuleName,SM.IconName,SM.IsActive,SM.CreatedBy,SM.CreatedDate,SM.UpdatedBy,SM.UpdatedDate,
                M.ModuleName
                FROM Security.SubModules SM
                INNER JOIN Security.Modules M ON M.ModuleId=SM.ModuleId
                WHERE SM.OrgId=ISNULL(@OrgId,SM.OrgId)";
            return _dbContext._connection.Query<SubModulesViewModel>(query, new { OrgId = orgId });
        }

        public int Update(SubModules entity)
        {
            var query = CRUD<SubModules>.Update(o => o.SubModuleId == o.SubModuleId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
