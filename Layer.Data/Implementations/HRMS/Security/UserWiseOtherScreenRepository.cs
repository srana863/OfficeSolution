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
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<UserWiseOtherScreen>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = InstituteId }).Single();
        }

        public UserWiseOtherScreen Get(int id, int InstituteId)
        {
            var query = CRUD<UserWiseOtherScreen>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<UserWiseOtherScreen>(query, new { SL = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<UserWiseOtherScreen> GetAll(int InstituteId)
        {
            var query = CRUD<UserWiseOtherScreen>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<UserWiseOtherScreen>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<UserWiseOtherScreenViewModel> GetAllWithParent(int InstituteId, int userId, int? moduleId, int? subModuleId)
        {
            var query = @"SELECT UWOS.SL,UWOS.UserId,UWOS.InstituteId,UWOS.ScreenCode,UWOS.CanAdd,UWOS.CanModify,UWOS.CanView,UWOS.IsActive,UWOS.AddedByUserId,UWOS.AddedDate,UWOS.UpdatedByUserId,UWOS.UpdatedDate,
                S.ModuleId,S.SubModuleId,S.SectionId,SM.SubModuleName,M.ModuleName,S.ScreenName
                FROM Security.UserWiseOtherScreen UWOS
                INNER JOIN Security.Screen S ON S.ScreenCode=UWOS.ScreenCode AND S.InstituteId=UWOS.InstituteId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.InstituteId=UWOS.InstituteId
                INNER JOIN Security.Modules M ON M.ModuleId=SM.ModuleId AND M.InstituteId=UWOS.InstituteId
                WHERE UWOS.InstituteId=@InstituteId
                AND UWOS.UserId=@UserId
                AND M.ModuleId=ISNULL(@ModuleId,M.ModuleId)
                AND SM.SubModuleId=ISNULL(@SubModuleId,SM.SubModuleId)";
            return _dbContext._connection.Query<UserWiseOtherScreenViewModel>(query, new
            {
                InstituteId = InstituteId,
                UserId=userId,
                ModuleId = moduleId > 0 ? moduleId : null,
                SubModuleId = subModuleId > 0 ? subModuleId : null,
            });
        }

        public int Update(UserWiseOtherScreen entity)
        {
            var query = CRUD<UserWiseOtherScreen>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

