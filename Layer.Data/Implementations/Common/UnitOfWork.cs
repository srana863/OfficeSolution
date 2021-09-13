using Layer.Data.Helpers;
using Layer.Data.Implementations.HRMS.Emp;
using Layer.Data.Interfaces;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Emp;
using Layer.Data.Interfaces.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Implementations
{
    public class UnitOfWork : UnitOfWorkContext, IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public UnitOfWork()
        {
            DepartmentRepository = new DepartmentRepository(_dbContext);
        }
    }
}
