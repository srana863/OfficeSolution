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
    public class UserRolesRepository : DataCommon, IUserRolesRepository
    {
        public UserRolesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(UserRoles entity)
        {
            var query = CRUD<UserRoles>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<UserRoles>.Delete(o => o.RoleId == o.RoleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { RoleId = id, InstituteId = InstituteId }).Single();
        }

        public UserRoles Get(int id, int InstituteId)
        {
            var query = CRUD<UserRoles>.Select(o => o.RoleId == o.RoleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<UserRoles>(query, new { RoleId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<UserRoles> GetAll(int InstituteId)
        {
            var query = CRUD<UserRoles>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<UserRoles>(query, new { InstituteId = InstituteId });
        }

        public int Update(UserRoles entity)
        {
            var query = CRUD<UserRoles>.Update(o => o.RoleId == o.RoleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
