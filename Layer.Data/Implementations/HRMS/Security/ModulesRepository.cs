using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Implementations.HRMS.Security
{
    public class ModulesRepository : DataCommon, IModulesRepository
    {
        public ModulesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Modules entity)
        {
            var query = CRUD<Modules>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<Modules>.Delete(o => o.ModuleId == o.ModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { ModuleId = id, InstituteId = InstituteId }).Single();
        }

        public Modules Get(int id, int InstituteId)
        {
            var query = CRUD<Modules>.Select(o => o.ModuleId == o.ModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Modules>(query, new { ModuleId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<Modules> GetAll(int InstituteId)
        {
            var query = CRUD<Modules>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Modules>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<ModuleViewModel> GetModulesWithSub()
        {
            var sql = @"SELECT m.ModuleId,m.ModuleName,m.IconName,sm.SubModuleId,sm.SubModuleName,sm.IconName,sm.ControllerName,sm.ActionName 
                        FROM Security.Modules m INNER JOIN Security.SubModules sm ON m.ModuleId=sm.ModuleId 
                        ORDER BY m.ModuleId";

            var moduleDictionary = new Dictionary<int, ModuleViewModel>();


            var list =  _dbContext._connection.Query<ModuleViewModel, SubModuleViewModel, ModuleViewModel>(
                sql,
                (s, sm) =>
                {
                    ModuleViewModel module;

                    if (!moduleDictionary.TryGetValue(s.ModuleId, out module))
                    {
                        module = s;
                        module.SubModuleViewModels = new List<SubModuleViewModel>();
                        moduleDictionary.Add(module.ModuleId, module);
                    }

                    module.SubModuleViewModels.Add(sm);
                    return module;
                },
                splitOn: "SubModuleId")
            .Distinct()
            .ToList();
            return list;
        }

        public int Update(Modules entity)
        {
            var query = CRUD<Modules>.Update(o => o.ModuleId == o.ModuleId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
