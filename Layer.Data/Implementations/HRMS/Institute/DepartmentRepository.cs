﻿using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.ViewModel.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
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
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Department>.Delete(o => o.DepartmentId == o.DepartmentId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { DepartmentId = id, InstituteId = instituteId }).Single();
        }

        public Department Get(int id, int instituteId)
        {
            var query = CRUD<Department>.Select(o => o.DepartmentId == o.DepartmentId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Department>(query, new { DepartmentId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<Department> GetAll(int instituteId)
        {
            var query = CRUD<Department>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Department>(query, new { InstituteId = instituteId });
        }

        public int Update(Department entity)
        {
            var query = CRUD<Department>.Update(o => o.DepartmentId == o.DepartmentId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }

        public DepartmentViewModel GetNothiId(int departmentId, int instituteId)
        {
            var query = @"SELECT CAST(ISNULL(ND.DeptSl,0) AS INT) DeptSl,D.DepartmentId,D.DeptAnchorName,D.DeptName,D.DeptNameBangla FROM Institute.Department D 
            LEFT JOIN (SELECT MAX(DeptSl) DeptSl,DepartmentId FROM Nothi.NothiDetails GROUP BY DepartmentId) ND ON ND.DepartmentId=D.DepartmentId
            WHERE D.DepartmentId=@DepartmentId";
            return _dbContext._connection.Query<DepartmentViewModel>(query, new { DepartmentId = departmentId, InstituteId = instituteId }).FirstOrDefault();
        }
    }
}
