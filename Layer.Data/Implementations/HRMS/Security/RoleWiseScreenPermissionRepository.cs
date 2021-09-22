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

        public IEnumerable<RoleWiseScreenPermissionViewModel> GetAllWithParent(int orgId, int roleId, int? moduleId, int? subModuleId)
        {
            var query = @"SELECT RWSP.PermissionSL,RWSP.RoleId,S.OrgId,S.ScreenCode,RWSP.CanAdd,RWSP.CanModify,RWSP.CanView,RWSP.IsActive,RWSP.CreatedBy,RWSP.CreatedDate,RWSP.UpdatedBy,RWSP.UpdatedDate,
                S.ModuleId,S.SubModuleId,S.SectionId,SMS.SectionName,SM.SubModuleName,M.ModuleName,S.ScreenName
                FROM Security.Screen S
                INNER JOIN Security.SubModuleSections SMS ON SMS.SectionId=S.SectionId AND SMS.OrgId=S.OrgId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.OrgId=S.OrgId
                INNER JOIN Security.Modules M ON M.ModuleId=S.ModuleId AND M.OrgId=S.OrgId
				LEFT JOIN Security.RoleWiseScreenPermission RWSP ON RWSP.ScreenCode=S.ScreenCode AND RWSP.OrgId=S.OrgId AND RWSP.RoleId=@RoleId
                WHERE S.OrgId=@OrgId
				AND S.ModuleId=ISNULL(@ModuleId,S.ModuleId)
				AND S.SubModuleId=ISNULL(@SubModuleId,S.SubModuleId)";
            return _dbContext._connection.Query<RoleWiseScreenPermissionViewModel>(query,
                new
                {
                    OrgId = orgId,
                    RoleId=roleId,
                    ModuleId = moduleId>0 ? moduleId : null,
                    SubModuleId = subModuleId > 0 ? subModuleId : null,
                });
        }

        public int Update(RoleWiseScreenPermission entity)
        {
            var query = CRUD<RoleWiseScreenPermission>.Update(o => o.PermissionSL == o.PermissionSL && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

