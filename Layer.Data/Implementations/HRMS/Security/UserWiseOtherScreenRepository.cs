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
    public class UserWiseOtherScreenRepository : DataCommon, IUserWiseOtherScreenRepository
    {
        public UserWiseOtherScreenRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(UserWiseOtherScreen entity)
        {
            var query = CRUD<UserWiseOtherScreen>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<UserWiseOtherScreen>.Delete(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SL = id, OrgId = orgId }).Single();
        }

        public UserWiseOtherScreen Get(int id, int orgId)
        {
            var query = CRUD<UserWiseOtherScreen>.Select(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<UserWiseOtherScreen>(query, new { SL = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<UserWiseOtherScreen> GetAll(int orgId)
        {
            var query = CRUD<UserWiseOtherScreen>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<UserWiseOtherScreen>(query, new { OrgId = orgId });
        }

        public IEnumerable<UserWiseOtherScreenViewModel> GetAllWithParent(int orgId, int userId, int? moduleId, int? subModuleId)
        {
            var query = @"SELECT UWOS.SL,UWOS.UserId,UWOS.OrgId,UWOS.ScreenCode,UWOS.CanAdd,UWOS.CanModify,UWOS.CanView,UWOS.IsActive,UWOS.CreatedBy,UWOS.CreatedDate,UWOS.UpdatedBy,UWOS.UpdatedDate,
                S.ModuleId,S.SubModuleId,S.SectionId,SM.SubModuleName,M.ModuleName,S.ScreenName
                FROM Security.UserWiseOtherScreen UWOS
                INNER JOIN Security.Screen S ON S.ScreenCode=UWOS.ScreenCode AND S.OrgId=UWOS.OrgId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.OrgId=UWOS.OrgId
                INNER JOIN Security.Modules M ON M.ModuleId=SM.ModuleId AND M.OrgId=UWOS.OrgId
                WHERE UWOS.OrgId=@OrgId
                AND UWOS.UserId=@UserId
                AND M.ModuleId=ISNULL(@ModuleId,M.ModuleId)
                AND SM.SubModuleId=ISNULL(@SubModuleId,SM.SubModuleId)";
            return _dbContext._connection.Query<UserWiseOtherScreenViewModel>(query, new
            {
                OrgId = orgId,
                UserId=userId,
                ModuleId = moduleId > 0 ? moduleId : null,
                SubModuleId = subModuleId > 0 ? subModuleId : null,
            });
        }

        public int Update(UserWiseOtherScreen entity)
        {
            var query = CRUD<UserWiseOtherScreen>.Update(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

