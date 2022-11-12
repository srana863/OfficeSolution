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
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Delete(o => o.PermissionSL == o.PermissionSL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { PermissionSL = id, InstituteId = InstituteId }).Single();
        }

        public RoleWiseScreenPermission Get(int id, int InstituteId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Select(o => o.PermissionSL == o.PermissionSL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<RoleWiseScreenPermission>(query, new { PermissionSL = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<RoleWiseScreenPermission> GetAll(int InstituteId)
        {
            var query = CRUD<RoleWiseScreenPermission>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<RoleWiseScreenPermission>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<RoleWiseScreenPermissionViewModel> GetAllWithParent(int InstituteId, int roleId, int? moduleId, int? subModuleId)
        {
            var query = @"SELECT RWSP.PermissionSL,RWSP.RoleId,S.InstituteId,S.ScreenCode,RWSP.CanAdd,RWSP.CanModify,RWSP.CanView,RWSP.IsActive,RWSP.AddedByUserId,RWSP.AddedDate,RWSP.UpdatedByUserId,RWSP.UpdatedDate,
                S.ModuleId,S.SubModuleId,S.SectionId,SMS.SectionName,SM.SubModuleName,M.ModuleName,S.ScreenName
                FROM Security.Screen S
                INNER JOIN Security.SubModuleSections SMS ON SMS.SectionId=S.SectionId AND SMS.InstituteId=S.InstituteId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.InstituteId=S.InstituteId
                INNER JOIN Security.Modules M ON M.ModuleId=S.ModuleId AND M.InstituteId=S.InstituteId
				LEFT JOIN Security.RoleWiseScreenPermission RWSP ON RWSP.ScreenCode=S.ScreenCode AND RWSP.InstituteId=S.InstituteId AND RWSP.RoleId=@RoleId
                WHERE S.InstituteId=@InstituteId
				AND S.ModuleId=ISNULL(@ModuleId,S.ModuleId)
				AND S.SubModuleId=ISNULL(@SubModuleId,S.SubModuleId)";
            return _dbContext._connection.Query<RoleWiseScreenPermissionViewModel>(query,
                new
                {
                    InstituteId = InstituteId,
                    RoleId=roleId,
                    ModuleId = moduleId>0 ? moduleId : null,
                    SubModuleId = subModuleId > 0 ? subModuleId : null,
                });
        }

        public int Update(RoleWiseScreenPermission entity)
        {
            var query = CRUD<RoleWiseScreenPermission>.Update(o => o.PermissionSL == o.PermissionSL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}

