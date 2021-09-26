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
    public class UserDetailsRepository : DataCommon, IUserDetailsRepository
    {
        public UserDetailsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(UserDetails entity)
        {
            var query = CRUD<UserDetails>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<UserDetails>.Delete(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { UserId = id, OrgId = orgId }).Single();
        }

        public UserDetails Get(int id, int orgId)
        {
            var query = CRUD<UserDetails>.Select(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<UserDetails>(query, new { UserId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<UserDetails> GetAll(int orgId)
        {
            var query = CRUD<UserDetails>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<UserDetails>(query, new { OrgId = orgId });
        }

        public int Update(UserDetails entity)
        {
            var query = CRUD<UserDetails>.Update(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
