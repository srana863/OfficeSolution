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
    public class NothiMovementRepository : DataCommon, INothiMovementRepository
    {
        public NothiMovementRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NothiMovement entity)
        {
            var query = CRUD<NothiMovement>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<NothiMovement>.Delete(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { NothiMovementId = id, InstituteId = InstituteId }).Single();
        }

        public NothiMovement Get(int id, int InstituteId)
        {
            var query = CRUD<NothiMovement>.Select(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiMovement>(query, new { NothiMovementId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<NothiMovement> GetAll(int InstituteId)
        {
            var query = CRUD<NothiMovement>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiMovement>(query, new { InstituteId = InstituteId });
        }

        public int Update(NothiMovement entity)
        {
            var query = CRUD<NothiMovement>.Update(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
