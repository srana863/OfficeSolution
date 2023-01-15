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
    public class NothiTypeRepository : DataCommon, INothiTypeRepository
    {
        public NothiTypeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NothiType entity)
        {
            var query = CRUD<NothiType>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<NothiType>.Delete(o => o.NothiTypeId == o.NothiTypeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { NothiTypeId = id, InstituteId = InstituteId }).Single();
        }

        public NothiType Get(int id, int InstituteId)
        {
            var query = CRUD<NothiType>.Select(o => o.NothiTypeId == o.NothiTypeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiType>(query, new { NothiTypeId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<NothiType> GetAll(int InstituteId)
        {
            var query = CRUD<NothiType>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiType>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<NothiTypeViewModel> GetAll(int InstituteId, int departmentId)
        {
            var query = @"SELECT NT.NothiTypeId,NT.InstituteId,NT.DepartmentId,NT.NothiTypeName,NT.NothiTypeNameBang,NT.IsActive,NT.AddedByUserId,NT.AddedDate,NT.UpdatedByUserId,NT.UpdatedDate,D.DeptName
                FROM Nothi.NothiType NT
                INNER JOIN Institute.Department D ON D.DepartmentId=NT.DepartmentId
                WHERE NT.InstituteId=ISNULL(@InstituteId,NT.InstituteId) AND NT.DepartmentId=ISNULL(@DepartmentId,NT.DepartmentId)";
            return _dbContext._connection.Query<NothiTypeViewModel>(query, new { InstituteId = InstituteId, DepartmentId = departmentId });
        }

        public int Update(NothiType entity)
        {
            var query = CRUD<NothiType>.Update(o => o.NothiTypeId == o.NothiTypeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
