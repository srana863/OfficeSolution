using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Model.Common;
using Layer.Model.HRMS.Emp;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Implementations.HRMS.Emp
{
    public class DepartmentRepository : DataCommon, IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public int Create(Department entity)
        {
            var query = CRUD<Department>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<Department>.Delete(o => o.DeptId == o.DeptId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { DeptId = id, OrgId = orgId }).Single();
        }

        public Department Get(int id, int orgId)
        {
            var query = CRUD<Department>.Select(o => o.DeptId == o.DeptId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Department>(query, new { DeptId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<Department> GetAll(int orgId)
        {
            var query = CRUD<Department>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<Department>(query, new { OrgId = orgId });
        }

        public int Update(Department entity)
        {
            var query = CRUD<Department>.Update(o => o.DeptId == o.DeptId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
