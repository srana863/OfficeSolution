using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Security;
using Layer.Model.ViewModel.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Security
{
    class SubModuleSectionsRepository : DataCommon, ISubModuleSectionsRepository
    {
        public SubModuleSectionsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(SubModuleSections entity)
        {
            var query = CRUD<SubModuleSections>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<SubModuleSections>.Delete(o => o.SectionId == o.SectionId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { SectionId = id, OrgId = orgId }).Single();
        }

        public SubModuleSections Get(int id, int orgId)
        {
            var query = CRUD<SubModuleSections>.Select(o => o.SectionId == o.SectionId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<SubModuleSections>(query, new { SectionId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<SubModuleSections> GetAll(int orgId)
        {
            var query = CRUD<SubModuleSections>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<SubModuleSections>(query, new { OrgId = orgId });
        }

        public IEnumerable<SubModuleSectionsViewModel> GetAllWithParent(int orgId)
        {
            var query = @"SELECT SMS.SectionId,SMS.SectionName,SMS.OrgId,SMS.ModuleId,SMS.SubModuleId,SMS.IconName,SMS.SectionOrder,SMS.IsActive,SMS.CreatedBy,SMS.CreatedDate,SMS.UpdatedBy,SMS.UpdatedDate,
                M.ModuleName,SM.SubModuleName
                FROM Security.SubModuleSections SMS
                INNER JOIN Security.Modules M ON M.ModuleId=SMS.ModuleId AND M.OrgId=SMS.OrgId
                INNER JOIN Security.SubModules SM ON SM.SubModuleId=SMS.SubModuleId AND SM.OrgId=SMS.OrgId
                WHERE SMS.OrgId=ISNULL(@OrgId,SMS.OrgId)";
            return _dbContext._connection.Query<SubModuleSectionsViewModel>(query, new { OrgId = orgId });
        }
        public IEnumerable<SectionViewModel> GetSectionsWithScreen(string actionName, string controllerName, bool home=true)
        {
            var sql = @"DECLARE @SubModuleId int
                        SET @SubModuleId = (SELECT SubModuleId FROM Security.SubModules
					                        WHERE ActionName= @ActionName AND ControllerName = @ControllerName
					                        )
                        SELECT s.SectionId,s.SectionName,s.IconName,sc.ScreenCode,sc.ScreenName,sc.URL,
                        sc.IconName,sc.ControllerName,sc.ActionName
                        FROM Security.SubModuleSections s 
                        INNER JOIN Security.Screen sc ON s.SectionId=sc.SectionId 
                        INNER JOIN Security.SubModules sm ON s.SubModuleId=sm.SubModuleId
                        WHERE sc.SubModuleId = @SubModuleId
                        ORDER BY s.SectionOrder ASC,sc.ScreenOrder ASC";

            var moduleDictionary = new Dictionary<int, SectionViewModel>();


            var list = _dbContext._connection.Query<SectionViewModel, Model.ViewModel.Settings.ScreenViewModel, SectionViewModel>(
                sql,
                (s, sm) =>
                {
                    SectionViewModel module;

                    if (!moduleDictionary.TryGetValue(s.SectionId, out module))
                    {
                        module = s;
                        module.ScreenViewModels = new List<Model.ViewModel.Settings.ScreenViewModel>();
                        moduleDictionary.Add(module.SectionId, module);
                    }

                    module.ScreenViewModels.Add(sm);
                    return module;
                },
                new { ActionName = actionName, ControllerName = controllerName },
                splitOn: "ScreenCode")
            .Distinct()
            .ToList();
            return list;
        }

        public IEnumerable<SectionViewModel> GetSectionsWithScreen(string actionName, string controllerName)
        {
            var sql = @"DECLARE @SubModuleId int
                        SET @SubModuleId = (SELECT SubModuleId FROM Security.Screen 
					                        WHERE ActionName= @ActionName AND ControllerName = @ControllerName
					                        )
                        SELECT s.SectionId,s.SectionName,s.IconName,sc.ScreenCode,sc.ScreenName,sc.URL,
                        sc.IconName,sc.ControllerName,sc.ActionName
                        FROM Security.SubModuleSections s 
                        INNER JOIN Security.Screen sc ON s.SectionId=sc.SectionId 
                        INNER JOIN Security.SubModules sm ON s.SubModuleId=sm.SubModuleId
                        WHERE sc.SubModuleId = @SubModuleId
                        ORDER BY s.SectionOrder ASC,sc.ScreenOrder ASC";

            var moduleDictionary = new Dictionary<int, SectionViewModel>();


            var list = _dbContext._connection.Query<SectionViewModel, Model.ViewModel.Settings.ScreenViewModel, SectionViewModel>(
                sql,
                (s, sm) =>
                {
                    SectionViewModel module;

                    if (!moduleDictionary.TryGetValue(s.SectionId, out module))
                    {
                        module = s;
                        module.ScreenViewModels = new List<Model.ViewModel.Settings.ScreenViewModel>();
                        moduleDictionary.Add(module.SectionId, module);
                    }

                    module.ScreenViewModels.Add(sm);
                    return module;
                },
                new { ActionName = actionName, ControllerName = controllerName },
                splitOn: "ScreenCode")
            .Distinct()
            .ToList();
            return list;
        }

        public int Update(SubModuleSections entity)
        {
            var query = CRUD<SubModuleSections>.Update(o => o.SectionId == o.SectionId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}


