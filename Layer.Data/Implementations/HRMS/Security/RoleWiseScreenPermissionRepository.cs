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
    public class RoleWiseScreenPermissionRepository : DataCommon, IRoleWiseScreenPermissionRepository
    {
        public RoleWiseScreenPermissionRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(RoleWiseScreenPermission entity)
        {
            var query = CRUD<RoleWiseScreenPermission>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Delete(o => o.PermissionSL == o.PermissionSL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { PermissionSL = id, OrgId = orgId }).Single();
        }

        public RoleWiseScreenPermission Get(int id, int orgId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Select(o => o.PermissionSL == o.PermissionSL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<RoleWiseScreenPermission>(query, new { PermissionSL = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<RoleWiseScreenPermission> GetAll(int orgId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<RoleWiseScreenPermission>(query, new { OrgId = orgId });
        }

        public int Update(RoleWiseScreenPermission entity)
        {
            var query = CRUD<RoleWiseScreenPermission>.Update(o => o.PermissionSL == o.PermissionSL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

