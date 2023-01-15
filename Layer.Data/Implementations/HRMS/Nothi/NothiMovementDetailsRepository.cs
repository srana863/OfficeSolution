using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Nothi;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Nothi;
using Layer.Model.HRMS.Security;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Nothi
{
    public class NothiMovementDetailsRepository : DataCommon, INothiMovementDetailsRepository
    {
        public NothiMovementDetailsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NothiMovementDetails entity)
        {
            var query = CRUD<NothiMovementDetails>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<NothiMovementDetails>.Delete(o => o.SL == o.SL );
            return _dbContext._connection.Query<int>(query, new { SL = id}).Single();
        }

        public NothiMovementDetails Get(int id, int InstituteId)
        {
            var query = CRUD<NothiMovementDetails>.Select(o => o.SL == o.SL );
            return _dbContext._connection.Query<NothiMovementDetails>(query, new { SL = id}).FirstOrDefault();
        }

        public IEnumerable<NothiMovementDetails> GetAll(int InstituteId)
        {
            var query = @"";
            return _dbContext._connection.Query<NothiMovementDetails>(query, new { InstituteId = InstituteId });
        }

        public int Update(NothiMovementDetails entity)
        {
            var query = CRUD<NothiMovementDetails>.Update(o => o.SL == o.SL );
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
