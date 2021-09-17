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

        public IEnumerable<UserWiseOtherScreenViewModel> GetAllWithParent(int orgId, int roleId, int? moduleId, int? subModuleId)
        {
            throw new NotImplementedException();
        }

        public int Update(UserWiseOtherScreen entity)
        {
            var query = CRUD<UserWiseOtherScreen>.Update(o => o.SL == o.SL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

