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

        public IEnumerable<ScreenViewModel> GetAllWithParent(int orgId)
        {
            var query = @"SELECT S.ScreenCode,S.ScreenName,S.OrgId,S.ModuleId,S.SubModuleId,S.SectionId,S.ScreenOrder,S.IconName,S.URL,S.ControllerName,S.ActionName,S.Description,S.IsActive,S.CreatedBy,S.CreatedDate,S.UpdatedBy,S.UpdatedDate,
                SMS.SectionName,SM.SubModuleName,M.ModuleName
                FROM Security.Screen S
                INNER JOIN Security.SubModuleSections SMS ON SMS.SectionId=S.SectionId AND SMS.OrgId=S.OrgId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.OrgId=S.OrgId
                INNER JOIN Security.Modules M ON M.ModuleId=S.ModuleId AND M.OrgId=S.OrgId
                WHERE S.OrgId=ISNULL(@OrgId,S.OrgId)";
            return _dbContext._connection.Query<ScreenViewModel>(query, new { OrgId = orgId });
        }

        public int Update(Screen entity)
        {
            var query = CRUD<Screen>.Update(o => o.ScreenCode == o.ScreenCode && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}


