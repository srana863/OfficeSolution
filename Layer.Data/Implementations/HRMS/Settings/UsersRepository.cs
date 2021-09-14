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
    public class UsersRepository : DataCommon, IUsersRepository
    {
        public UsersRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Users entity)
        {
            var query = CRUD<Users>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<Users>.Delete(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { UserId = id, OrgId = orgId }).Single();
        }

        public Users Get(int id, int orgId)
        {
            var query = CRUD<Users>.Select(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Users>(query, new { UserId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<Users> GetAll(int orgId)
        {
            var query = CRUD<Users>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Users>(query, new { OrgId = orgId });
        }

        public UserInfoSession GetUserByUserName(string username)
        {
            var query = CRUD<Users>.Select(o => o.Username == o.Username);
            return _dbContext._connection.Query<UserInfoSession>(query, new { Username = username }).FirstOrDefault();
        }

        public int Update(Users entity)
        {
            var query = CRUD<Users>.Update(o => o.UserId == o.UserId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
