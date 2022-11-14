using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
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
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<Screen>.Delete(o => o.ScreenCode == o.ScreenCode && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { ScreenCode = id, InstituteId = InstituteId }).Single();
        }

        public Screen Get(int id, int InstituteId)
        {
            var query = CRUD<Screen>.Select(o => o.ScreenCode == o.ScreenCode && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Screen>(query, new { ScreenCode = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<Screen> GetAll(int InstituteId)
        {
            var query = CRUD<Screen>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Screen>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<ScreenViewModel> GetAllWithParent(int InstituteId)
        {
            var query = @"SELECT S.ScreenCode,S.ScreenName,S.InstituteId,S.ModuleId,S.SubModuleId,S.SectionId,S.ScreenOrder,S.IconName,S.URL,S.ControllerName,S.ActionName,S.Description,S.IsActive,S.AddedByUserId,S.AddedDate,S.UpdatedByUserId,S.UpdatedDate,
                SMS.SectionName,SM.SubModuleName,M.ModuleName
                FROM Security.Screen S
                INNER JOIN Security.SubModuleSections SMS ON SMS.SectionId=S.SectionId AND SMS.InstituteId=S.InstituteId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=S.SubModuleId AND SM.InstituteId=S.InstituteId
                INNER JOIN Security.Modules M ON M.ModuleId=S.ModuleId AND M.InstituteId=S.InstituteId
                WHERE S.InstituteId=ISNULL(@InstituteId,S.InstituteId)";
            return _dbContext._connection.Query<ScreenViewModel>(query, new { InstituteId = InstituteId });
        }

        public Screen GetModuleDetailsByControllerName(string controllerName)
        {
            var query = @"SELECT S.ScreenCode,S.ScreenName,S.InstituteId,S.ModuleId,S.SubModuleId,S.SectionId,S.ScreenOrder,S.IconName,S.URL,S.ControllerName,S.ActionName,S.Description,S.IsActive,S.AddedByUserId,S.AddedDate,S.UpdatedByUserId,S.UpdatedDate
                FROM Security.Screen S
                WHERE S.IsActive=1
                AND S.ControllerName=@ControllerName";
            return _dbContext._connection.Query<Screen>(query, new { ControllerName = controllerName }).FirstOrDefault();
        }

        public int Update(Screen entity)
        {
            var query = CRUD<Screen>.Update(o => o.ScreenCode == o.ScreenCode && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}


