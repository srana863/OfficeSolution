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
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<SubModules>.Delete(o => o.SubModuleId == o.SubModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SubModuleId = id, InstituteId = InstituteId }).Single();
        }

        public SubModules Get(int id, int InstituteId)
        {
            var query = CRUD<SubModules>.Select(o => o.SubModuleId == o.SubModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<SubModules>(query, new { SubModuleId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<SubModules> GetAll(int InstituteId)
        {
            var query = CRUD<SubModules>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<SubModules>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<SubModulesViewModel> GetAllWithParent(int InstituteId)
        {
            var query = @"SELECT SM.SubModuleId,SM.InstituteId,SM.ModuleId,SM.SubModuleName,SM.IconName,SM.IsActive,SM.AddedByUserId,SM.AddedDate,SM.UpdatedByUserId,SM.UpdatedDate,
                M.ModuleName
                FROM Security.SubModules SM
                INNER JOIN Security.Modules M ON M.ModuleId=SM.ModuleId
                WHERE SM.InstituteId=ISNULL(@InstituteId,SM.InstituteId)";
            return _dbContext._connection.Query<SubModulesViewModel>(query, new { InstituteId = InstituteId });
        }

        public int Update(SubModules entity)
        {
            var query = CRUD<SubModules>.Update(o => o.SubModuleId == o.SubModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
