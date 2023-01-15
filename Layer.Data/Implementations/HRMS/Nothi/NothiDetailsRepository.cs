using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Nothi;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Nothi;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Nothi;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Nothi
{
    public class NothiDetailsRepository : DataCommon, INothiDetailsRepository
    {
        public NothiDetailsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NothiDetails entity)
        {
            var query = CRUD<NothiDetails>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<NothiDetails>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = InstituteId }).Single();
        }

        public NothiDetails Get(int id, int InstituteId)
        {
            var query = CRUD<NothiDetails>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiDetails>(query, new { SL = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<NothiDetails> GetAll(int InstituteId)
        {
            var query = CRUD<NothiDetails>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiDetails>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<NothiDetailsViewModel> GetAll(int InstituteId, int departmentId)
        {
            var query = @"";
            return _dbContext._connection.Query<NothiDetailsViewModel>(query, new { InstituteId = InstituteId, DepartmentId = departmentId });
        }

        public int Update(NothiDetails entity)
        {
            var query = CRUD<NothiDetails>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
